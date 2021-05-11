using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Models
{
    public class HackerNewsUpdates
    {
        [JsonPropertyName("items")]
        public List<int> Items { get; set; }
        [JsonPropertyName("profiles")]
        public List<string> Profiles { get; set; }
    }
}
