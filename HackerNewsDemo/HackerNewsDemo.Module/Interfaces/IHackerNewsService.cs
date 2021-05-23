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
    /// <summary>
    /// Interface for Hacker News Service Methods
    /// </summary>
    public interface IHackerNewsService
    {
        /// <summary>
        /// Retrieve Hacker News Item by Item ID
        /// </summary>
        /// <param name="itemId">int</param>
        /// <returns>Hacker News Item Domain Object</returns>
        Task<HackerNewsItemDomain> GetHackerNewsItem(int itemId);
        /// <summary>
        /// Retrieve Hacker News Max Item ID
        /// </summary>
        /// <returns>Max Hacker News Item ID</returns>
        Task<int> GetHackerNewsMaxItem();
        /// <summary>
        /// Retrieve a list of Hacker News IDs based on Story Type
        /// </summary>
        /// <param name="type">Story Type Enum</param>
        /// <returns>List of Hacker News Item IDs</returns>
        Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type);
        /// <summary>
        /// Retrieve a list of Hacker News Items based on list of Item IDs
        /// </summary>
        /// <remarks>
        /// This service method will retrieve Hacker News Items by first looking at InMemory DB
        /// if it has not been found the method will insert the item into DB
        /// </remarks>
        /// <param name="Ids">List of int</param>
        /// <returns>List of Hacker News Item Domain</returns>
        Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(List<int> Ids);
        /// <summary>
        /// Retrieve a list of Hacker News Items based on list of Item IDs
        /// </summary>
        /// <remarks>
        /// Relevant child Hacker News Item IDs are stored as strings
        /// </remarks>
        /// <param name="Ids">comma separated string</param>
        /// <returns>List of Hacker News Item Domain</returns>
        Task<List<HackerNewsItemDomain>> GetHackerNewsItemList(string Ids);
        /// <summary>
        /// Retrieve Hacker News Item Updates
        /// </summary>
        /// <remarks>
        /// Uncertain of frequency Hacker News API updates this dataset
        /// </remarks>
        /// <returns>Updated Items and User profiles</returns>
        Task<HackerNewsUpdates> GetHackerNewsItemUpdates();
        /// <summary>
        /// Search through Hacker News Item Titles and Text for matching words
        /// </summary>
        /// <remarks>
        /// Ignores case sensitivity. Is not searching for whole words
        /// </remarks>
        /// <param name="searchTerms"></param>
        /// <returns>List of Hacker News Item Domain</returns>
        Task<List<HackerNewsItemDomain>> SearchHackerNewsItems(string searchTerms);
        /// <summary>
        /// Search through Hacker News Item Titles and Text for matching whole words
        /// </summary>
        /// <remarks>
        /// Ignores case sensitivity.
        /// </remarks>
        /// <param name="searchTerms"></param>
        /// <returns>List of Hacker News Item Domain</returns>
        Task<List<HackerNewsItemDomain>> SearchExactWordsHackerNewsItems(string searchTerms);
        /// <summary>
        /// Background Service fired when application excutes an intial request to update
        /// all items in the InMemory DB if it exist
        /// </summary>
        /// <remarks>
        /// Timed Background Service fired wvery 30 seconds.
        /// </remarks>
        void UpdateItemsBackgroundService();
    }
}
