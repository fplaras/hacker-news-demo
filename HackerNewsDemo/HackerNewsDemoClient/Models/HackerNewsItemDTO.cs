using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackerNewsDemoClient.Models
{
    public class HackerNewsItemDTO
    {
        [JsonPropertyName("by")]
        public string By { get; set; }

        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("parent")]
        public int Parent { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        [JsonPropertyName("kids")]
        public List<HackerNewsItemDTO> Kids { get; set; }
        [JsonPropertyName("parts")]
        public List<HackerNewsItemDTO> Parts { get; set; }
    }
}
