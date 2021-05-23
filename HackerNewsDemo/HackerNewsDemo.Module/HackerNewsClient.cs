using HackerNewsDemo.Module.Enums;
using HackerNewsDemo.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpResponseException = HackerNewsDemo.Module.Models.HttpResponseException;

namespace HackerNewsDemo.Module
{
    /// <summary>
    /// Hacker News Client for API request to Hacker News API
    /// </summary>
    public class HackerNewsClient
    {
        public HttpClient Client { get; }

        public HackerNewsClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            Client = client;
        }

        public async Task<HackerNewsItem> GetItem(int Id)
        {
            try
            {
                var response = await Client.GetAsync("item/" + Id + ".json");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                HackerNewsItem itemList = JsonSerializer.Deserialize<HackerNewsItem>(json);

                return itemList;
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Critical Error: {0} InnerException: {1}", ex.Message, ex.InnerException.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError, msg);
            }

        }

        public async Task<int> GetMaxItem()
        {
            try
            {
                var response = await Client.GetAsync("maxitem.json");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                int maxItem = JsonSerializer.Deserialize<int>(json);

                return maxItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<HackerNewsUpdates> GetHackerNewsUpdates()
        {
            try
            {
                var response = await Client.GetAsync("updates.json");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                HackerNewsUpdates updates = JsonSerializer.Deserialize<HackerNewsUpdates>(json);

                return updates;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<int>> GetStoriesByType(HackerNewsEnums.StoryType type)
        {
            try
            {
                var response = await Client.GetAsync(type + ".json");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                List<int> storyList = JsonSerializer.Deserialize<List<int>>(json);

                return storyList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
