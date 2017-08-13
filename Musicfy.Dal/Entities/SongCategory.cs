using Newtonsoft.Json;

namespace Musicfy.Dal.Entities
{
    public class SongCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}