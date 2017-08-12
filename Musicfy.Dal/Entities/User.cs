using Newtonsoft.Json;

namespace Musicfy.Dal.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}