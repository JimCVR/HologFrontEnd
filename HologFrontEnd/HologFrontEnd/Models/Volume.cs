using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    public class VolumeInfo
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("imageLinks")]
        public imageLink imageLinks { get; set; } = new imageLink();

        [JsonProperty("averageRating")]
        public string score { get; set; }

        [JsonProperty("publishedDate")]
        public string date { get; set; }
    }
}
