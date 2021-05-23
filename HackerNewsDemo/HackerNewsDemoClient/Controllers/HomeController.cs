using HackerNewsDemoClient.Enums;
using HackerNewsDemoClient.Models;
using HackerNewsDemoClient.TypedClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsDemoClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HackerNewsClient _client;

        public HomeController(ILogger<HomeController> logger, HackerNewsClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Search(string searchTerms, int pageNumber = 1, int pageSize = 30)
        {
            HomeViewModel vm = new HomeViewModel();
            if (!string.IsNullOrEmpty(searchTerms))
            {
                var x = await _client.GetSearchHackerNewsItems(searchTerms, pageNumber, pageSize);

                vm.HackerNewsItems = x;
                vm.PageSize = pageSize;
                vm.CurrentPage = pageNumber;
                vm.SearchTerms = searchTerms;
            }

            return View(vm);
        }

        public async Task<IActionResult> NewStories(int pageNumber = 1, int pageSize = 30)
        {
            var x = await _client.GetHackerNewsItems(HackerNewsEnums.StoryType.newstories, pageNumber, pageSize);

            HomeViewModel vm = new HomeViewModel
            {
                HackerNewsItems = x,
                PageSize = pageSize,
                CurrentPage = pageNumber,
            };

            return View(vm);
        }

        public async Task<IActionResult> TopStories(int pageNumber = 1, int pageSize = 30)
        {
            var x = await _client.GetHackerNewsItems(HackerNewsEnums.StoryType.topstories, pageNumber, pageSize);

            HomeViewModel vm = new HomeViewModel
            {
                HackerNewsItems = x,
                PageSize = pageSize,
                CurrentPage = pageNumber,
            };

            return View(vm);
        }

        public async Task<IActionResult> BestStories(int pageNumber = 1, int pageSize = 30)
        {
            var x = await _client.GetHackerNewsItems(HackerNewsEnums.StoryType.beststories, pageNumber, pageSize);

            HomeViewModel vm = new HomeViewModel
            {
                HackerNewsItems = x,
                PageSize = pageSize,
                CurrentPage = pageNumber,
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
