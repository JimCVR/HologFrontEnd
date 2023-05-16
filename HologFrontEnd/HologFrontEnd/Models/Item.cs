using Android.App;
using Newtonsoft.Json;
using System;

namespace HologFrontEnd.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("author")]
        public string author { get; set; }
    }
}
