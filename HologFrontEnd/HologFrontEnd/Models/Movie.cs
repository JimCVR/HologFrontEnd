using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    class Movie
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("overview")]
        public string overview { get; set; }

        [JsonProperty("poster_path")]
        public string poster { get; set; }

        [JsonProperty("vote_average")]
        public string vote { get; set; }

        [JsonProperty("release_date")]
        public string releaseDate { get; set; }
    }
}
