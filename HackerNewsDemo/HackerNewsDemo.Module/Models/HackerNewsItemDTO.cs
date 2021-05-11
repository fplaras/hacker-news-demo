using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Models
{
    public class HackerNewsItemDTO
    {
        public int Id { get; set; }

        public string By { get; set; }

        public int Descendants { get; set; }

        public int Score { get; set; }

        public int Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }
        public string Text { get; set; }

        public List<HackerNewsItemDTO> Kids { get; set; }

        public List<HackerNewsItemDTO> Parts { get; set; }
    }
}
