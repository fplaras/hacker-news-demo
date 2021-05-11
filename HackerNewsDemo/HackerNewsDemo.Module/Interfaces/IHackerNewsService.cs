using HackerNewsDemo.Module.Domain;
using HackerNewsDemo.Module.Enums;
using HackerNewsDemo.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Interfaces
{
    public interface IHackerNewsService
    {
        Task<HackerNewsItemDomain> GetHackerNewsItem(int itemId);
        Task<int> GetHackerNewsMaxItem();
        Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type);
        Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(List<int> Ids);
        Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(string Ids);
        Task<HackerNewsUpdates> GetHackerNewsItemUpdates();
        Task<List<HackerNewsItemDomain>> SearchHackerNewsItems(string searchTerms);
        void UpdateItemsBackgroundService();
    }
}
