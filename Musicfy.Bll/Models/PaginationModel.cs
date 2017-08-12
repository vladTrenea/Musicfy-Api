using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class PaginationModel<T>
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }
}