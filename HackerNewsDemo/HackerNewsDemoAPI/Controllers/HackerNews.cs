using AutoMapper;
using HackerNewsDemo.Module.Enums;
using HackerNewsDemo.Module.Interfaces;
using HackerNewsDemo.Module.Models;
using HackerNewsDemo.Module.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsDemoAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class HackerNews : ControllerBase
    {
        private readonly IHackerNewsService _service;
        private readonly IMapper _mapper;

        public HackerNews(IHackerNewsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        /// <summary>
        /// Get Hacker News Item
        /// </summary>
        /// <remarks>
        /// Will retrieve child item details if they exist
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Hacker News Item Details</returns>
        [HttpGet("item/{id}")]
        public async Task<IActionResult> Item([FromRoute] int id)
        {
            var item = await _service.GetHackerNewsItem(id);
            var hackerNewsDTO = _mapper.Map<HackerNewsItemDTO>(item);

            if (!string.IsNullOrEmpty(item.Kids))
            {
                var itemChildren = await _service.GetHackerNewsItemList(item.Kids);
                hackerNewsDTO.Kids = _mapper.Map<List<HackerNewsItemDTO>>(itemChildren); ;
            }

            if (!string.IsNullOrEmpty(item.Parts))
            {
                var itemParts = await _service.GetHackerNewsItemList(item.Parts);
                hackerNewsDTO.Kids = _mapper.Map<List<HackerNewsItemDTO>>(itemParts); ;
            }

            return Ok(hackerNewsDTO);
        }

        /// <summary>
        /// Get Hacker News Max Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hacker News Item Details</returns>
        [HttpGet("maxitem")]
        [ResponseCache(Duration = 30, Location= ResponseCacheLocation.Any)]
        public async Task<IActionResult> MaxItem([FromServices] HackerNewsService service)
        {
            var itemId = await _service.GetHackerNewsMaxItem();
            return Ok(itemId);
        }

        /// <summary>
        /// Get list of hacker news item by story type
        /// </summary>
        /// <param name="storyType"></param>
        /// <param name="iteration"></param>
        /// <param name="requestSize"></param>
        /// <returns>List of hacker news items</returns>
        [HttpGet("all/{storyType}/details")]
        public async Task<IActionResult> GetStoriesAndDetails([FromRoute] string storyType,
            [FromQuery] int iteration = 1, [FromQuery] int responseSize = 30)
        {
            if (Enum.IsDefined(typeof(HackerNewsEnums.StoryType), storyType))
            {
                var itemList = await _service.GetHackerNewsStories((HackerNewsEnums.StoryType)Enum.Parse(typeof(HackerNewsEnums.StoryType), storyType));
                itemList = itemList.Skip((iteration - 1) * responseSize).Take(responseSize).ToList();
                var stories = await _service.GetHackerNewsItemList(itemList);
                return Ok(_mapper.Map<List<HackerNewsItemDTO>>(stories));
            }
            else
            {
                return BadRequest("Story Type does not exist.");
            }
        }

        /// <summary>
        /// Get hacker news items by a search term for new stories only
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <param name="iteration"></param>
        /// <param name="requestSize"></param>
        /// <returns>List of hacker news item details</returns>
        [HttpGet("all/search/details")]
        public async Task<IActionResult> GetStoriesBySearchParams([FromQuery] string searchTerms, 
            [FromQuery] int iteration = 1, [FromQuery] int requestSize = 30)
        {
                var s = await _service.SearchHackerNewsItems(searchTerms);
                s = s.Skip((iteration - 1) * requestSize).Take(requestSize).ToList();
                return Ok(_mapper.Map<List<HackerNewsItemDTO>>(s));
           
        }


    }
}
