using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class ArtistModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}