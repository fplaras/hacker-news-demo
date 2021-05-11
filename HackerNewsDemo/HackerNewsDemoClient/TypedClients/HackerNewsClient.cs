using HackerNewsDemoClient.Enums;
using HackerNewsDemoClient.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HackerNewsDemoClient.TypedClients
{
    public class HackerNewsClient
    {
        public HttpClient Client { get; }
        private readonly IConfiguration _config;

        public HackerNewsClient(HttpClient client, IConfiguration config)
        {
            _config = config;
            client.BaseAddress = new Uri(_config["HackerNewsAPIv1"]);
            Client = client;
        }

        public async Task<List<HackerNewsItemDTO>> GetHackerNewsItems(HackerNewsEnums.StoryType type, int iteration, int responseSize)
        {
            var queryParams = QueryHelpers.AddQueryString("all/" + type + "/details", 
                new Dictionary<string, string> 
                {
                    { "iteration", iteration.ToString() },
                    { "responseSize", responseSize.ToString() },
                });

            var response = await Client.GetAsync(queryParams);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            List<HackerNewsItemDTO> itemList = JsonSerializer.Deserialize<List<HackerNewsItemDTO>>(json);

            return itemList;
        }

        public async Task<List<HackerNewsItemDTO>> GetSearchHackerNewsItems(string searchParams, int iteration, int responseSize)
        {
            var queryParams = QueryHelpers.AddQueryString("all/search/details",
                new Dictionary<string, string>
                {
                    { "searchTerms", searchParams },
                    { "iteration", iteration.ToString() },
                    { "responseSize", responseSize.ToString() },
                });

            var response = await Client.GetAsync(queryParams);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            List<HackerNewsItemDTO> itemList = JsonSerializer.Deserialize<List<HackerNewsItemDTO>>(json);

            return itemList;
        }
    }
}
