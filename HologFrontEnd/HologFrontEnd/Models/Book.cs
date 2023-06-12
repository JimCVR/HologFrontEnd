using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    class Book
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("volumeInfo")]
        public VolumeInfo volumeInfo { get; set; }

    }
    
}

