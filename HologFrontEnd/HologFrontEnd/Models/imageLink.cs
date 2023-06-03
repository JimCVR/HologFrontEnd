using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Models
{
    public class imageLink
    {
        [JsonProperty("smallThumbnail")]
        public string smallthumbnail { get; set; } = "";
    }
}
