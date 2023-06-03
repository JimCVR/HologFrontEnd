using Android.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        
        [JsonProperty("picture")]
        public string picture { get; set; }       
        [JsonProperty("score")]
        public string score { get; set; }       
        
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("custom")]
        public bool custom { get; set; }

        [JsonProperty("categoryId")]
        public string categoryId{ get; set; }
    }
}
