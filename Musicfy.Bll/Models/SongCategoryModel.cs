﻿using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class SongCategoryModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}