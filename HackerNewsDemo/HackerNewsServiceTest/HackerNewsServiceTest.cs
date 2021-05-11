using AutoMapper;
using HackerNewsDemo.Module;
using HackerNewsDemo.Module.Domain;
using HackerNewsDemo.Module.Enums;
using HackerNewsDemo.Module.Interfaces;
using HackerNewsDemo.Module.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsServiceTest
{
    public class HackerNewsServiceTest
    {
        private readonly IHackerNewsService _service;
        private HttpClient client = new HttpClient();
        private readonly IMapper _mapper;
        private readonly HackerNewsDataContext _context;

        public HackerNewsServiceTest()
        {
            var builder = new DbContextOptionsBuilder<HackerNewsDataContext>()
             .UseInMemoryDatabase("TestDB");
            _context = new HackerNewsDataContext(builder.Options);
            _service = new HackerNewsService(new HackerNewsClient(client), _context, _mapper);
        }

        [Fact]
        public async Task GetHackerNewsStories()
        {
            
                List<int> stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.newstories);
                Assert.True(stories.Count > 0, "Retrieved New Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.askstories);
                Assert.True(stories.Count > 0, "Retrieved Ask Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.beststories);
                Assert.True(stories.Count > 0, "Retrieved Best Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.topstories);
                Assert.True(stories.Count > 0, "Retrieved Top Stories");
        }

        [Fact]
        public async Task GetHackerNewsMaxItem()
        {
            
                var maxItem = await _service.GetHackerNewsMaxItem();
                Assert.True(maxItem > 0, "Max Item ID Retrieved");
            
        }

        [Fact]
        public async Task GetHackerNewsItem()
        {
                var maxItem = await _service.GetHackerNewsMaxItem();
                if (maxItem > 0)
                {
                    var item = await _service.GetHackerNewsItem(maxItem);

                    Assert.True(item.Id > 0, "Max Item Retrievable");
                }
                else
                {
                    Assert.False(maxItem == 0,"Invalid Max Item");
                }
            
        }
    }
}
