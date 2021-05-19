using AutoMapper;
using HackerNewsDemo.Module.Domain;
using HackerNewsDemo.Module.Enums;
using HackerNewsDemo.Module.Interfaces;
using HackerNewsDemo.Module.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HackerNewsClient _client;
        private readonly HackerNewsDataContext _context;
        private readonly IMapper _mapper;

        public HackerNewsService(HackerNewsClient client, HackerNewsDataContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        public async Task<HackerNewsItemDomain> GetHackerNewsItem(int itemId)
        {
            try
            {
                HackerNewsItem item = await _client.GetItem(itemId);
                HackerNewsItemDomain dItem = _mapper.Map<HackerNewsItemDomain>(item);
                return dItem;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(List<int> Ids)
        {
            try
            {
                List<HackerNewsItemDomain> itemList = new List<HackerNewsItemDomain>();

                foreach (var x in Ids)
                {
                    var dItem = await _context.HackerNewsItem.FindAsync(x);

                    if (dItem == null)
                    {
                        HackerNewsItemDomain item = await GetHackerNewsItem(x);

                        if(item != null)
                        {
                            var newItem = _mapper.Map<HackerNewsItemDomain>(item);
                            await _context.HackerNewsItem.AddAsync(newItem);
                            itemList.Add(newItem);
                        }
                    }
                    else
                    {
                        itemList.Add(dItem);
                    }
                }

                await _context.SaveChangesAsync();

                return itemList;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(string Ids)
        {
            try
            {
                List<HackerNewsItemDomain> items = new List<HackerNewsItemDomain>();
                List<int> itemIds = Ids.Split(',').Select(int.Parse).ToList();
                
                foreach(var itemId in itemIds)
                {
                    HackerNewsItemDomain item = await GetHackerNewsItem(itemId);

                    if(item != null)
                        items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<HackerNewsUpdates> GetHackerNewsItemUpdates()
        {
            try
            {
                return await _client.GetHackerNewsUpdates();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<int> GetHackerNewsMaxItem()
        {
            try
            {
                return await _client.GetMaxItem();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type)
        {
            try
            {
                return await _client.GetStoriesByType(type);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
        }

        public async Task<List<HackerNewsItemDomain>> SearchHackerNewsItems(string searchTerms)
        {
            try
            {
                List<string> keyword = searchTerms.Split(" ").ToList();

                List<HackerNewsItemDomain> filteredItems = new List<HackerNewsItemDomain>();

                List<HackerNewsItemDomain> filteredItemsText =
                    await _context.HackerNewsItem.Where(x => x.Text != null).ToListAsync();

                var filteredItemsTextResults = filteredItemsText.Where(x => keyword.Any(term => x.Text.Contains(term))).ToList();                    

                List<HackerNewsItemDomain> filteredItemTitles =
                    await _context.HackerNewsItem.Where(x => x.Title != null).ToListAsync();

                var filteredItemsTitlesResults = filteredItemTitles.Where(x => keyword.Any(term => x.Title.Contains(term, StringComparison.InvariantCultureIgnoreCase))).ToList();

                foreach (var item in filteredItemsTextResults)
                {
                    if (filteredItems.FirstOrDefault(x => x.Id == item.Id) == null)
                    {
                        filteredItems.Add(item);
                    }
                }

                foreach (var item in filteredItemsTitlesResults)
                {
                    if (filteredItems.FirstOrDefault(x => x.Id == item.Id) == null)
                    {
                        filteredItems.Add(item);
                    }
                }

                return filteredItems;
            }
            catch (Exception ex)
            {

                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }
            
        }

        public void UpdateItemsBackgroundService()
        {
            var itemList = GetHackerNewsStories(HackerNewsEnums.StoryType.newstories).Result;

            foreach (var itemId in itemList)
            {
                //get item
                var item = GetHackerNewsItem(itemId).Result;

                var domainItem = _context.HackerNewsItem.FindAsync(itemId).Result;

                if (domainItem != null)
                {
                    domainItem.Title = item.Title;
                    domainItem.Text = item.Text;
                    domainItem.Score = item.Score;
                    domainItem.Descendants = item.Descendants;
                    domainItem.Deleted = item.Deleted;
                    domainItem.Kids = item.Kids;
                    domainItem.Parts = item.Parts;

                    _context.HackerNewsItem.Update(domainItem);
                    _context.SaveChanges();
                }
            }
        }
    }
}
