using Android.App;
using Newtonsoft.Json;

namespace HologFrontEnd.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public long Id {  get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("iconId")]
        public long IconId { get; set; }

        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
