using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    class Videogame
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("metacritic")]
        public string score { get; set; }

        [JsonProperty("background_image")]
        public string image { get; set; }

        [JsonProperty("released")]
        public string date { get; set; }
    }
}
