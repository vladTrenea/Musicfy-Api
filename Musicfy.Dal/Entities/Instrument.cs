using Newtonsoft.Json;

namespace Musicfy.Dal.Entities
{
    public class Instrument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}