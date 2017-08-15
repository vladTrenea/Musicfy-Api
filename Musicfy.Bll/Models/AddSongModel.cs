using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class AddSongModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("artistId")]
        public string ArtistId { get; set; }

        [JsonProperty("songCategoryId")]
        public string SongCategoryId { get; set; }

        [JsonProperty("instrumentIds")]
        public IEnumerable<string> InstrumentIds { get; set; }

        [JsonProperty("tagIds")]
        public IEnumerable<string> TagIds { get; set; }
    }
}