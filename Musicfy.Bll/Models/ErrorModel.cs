using Newtonsoft.Json;

namespace Musicfy.Bll.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            
        }

        public ErrorModel(string message)
        {
            this.Message = message;
        }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}