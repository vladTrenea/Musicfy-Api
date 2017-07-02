using System.Linq;
using System.Net.Http;
using Musicfy.Infrastructure.Configs;

namespace MusicfyApi.Utils
{
    public static class RequestExtractor
    {
        public static string GetToken(HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (!headers.Contains(Constants.AuthHeader))
            {
                return null;
            }

            var accessToken = headers.GetValues(Constants.AuthHeader).FirstOrDefault();

            return accessToken;
        }
    }
}