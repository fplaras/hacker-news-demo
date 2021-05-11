using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsDemoClient.Models
{
    public class HomeViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<HackerNewsItemDTO> HackerNewsItems { get; set; }
        public string SearchTerms { get; set; }
    }
}
