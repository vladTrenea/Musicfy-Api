using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class SongModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("artist")]
        public ArtistModel Artist { get; set; }

        [JsonProperty("songCategory")]
        public SongCategoryModel SongCategory { get; set; }

        [JsonProperty("instruments")]
        public IEnumerable<InstrumentModel> Instruments { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<TagModel> Tags { get; set; }
    }
}