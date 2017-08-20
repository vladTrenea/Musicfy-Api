using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musicfy.Dal.Entities
{
    public class Song
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("artist")]
        [JsonIgnore]
        public Artist Artist { get; set; }

        [JsonProperty("instruments")]
        [JsonIgnore]
        public IEnumerable<Instrument> Instruments { get; set; }

        [JsonProperty("songCategory")]
        [JsonIgnore]
        public SongCategory SongCategory { get; set; }

        [JsonProperty("tags")]
        [JsonIgnore]
        public IEnumerable<Tag> Tags { get; set; }

        [JsonProperty("supporters")]
        [JsonIgnore]
        public IEnumerable<User> Supporters { get; set; }
    }
}