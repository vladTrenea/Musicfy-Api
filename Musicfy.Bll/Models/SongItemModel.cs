﻿using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class SongItemModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("artist")]
        public ArtistModel Artist { get; set; }
    }
}