using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class SongsDiscoverModel
    {
        [JsonProperty("matchedSong")]
        public SongItemModel MatchedSong { get; set; }

        [JsonProperty("recommendedSongs")]
        public IEnumerable<SongItemModel> RecommendedSongs { get; set; }
    }
}