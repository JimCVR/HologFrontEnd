using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    internal class Series
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string title { get; set; }

        [JsonProperty("overview")]
        public string overview { get; set; }

        [JsonProperty("poster_path")]
        public string poster { get; set; }

        [JsonProperty("vote_average")]
        public string vote { get; set; }

        [JsonProperty("first_air_date")]
        public string releaseDate { get; set; }
    }
}
