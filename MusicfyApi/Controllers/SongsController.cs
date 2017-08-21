using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Infrastructure.Configs;
using MusicfyApi.Attributes;
using MusicfyApi.Utils;

namespace MusicfyApi.Controllers
{
    public class SongsController : ApiController
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        [CustomAuthorize]
        public PaginationModel<SongItemModel> Get(int pageNumber)
        {
            return _songService.GetPaginated(pageNumber, Config.SongsPageCount);
        }

        [HttpGet]
        [CustomAuthorize]
        public SongModel Get([FromUri] string id)
        {
            return _songService.GetById(id);
        }

        [HttpPost]
        [CustomAuthorize(true)]
        public void Post([FromBody] AddUpdateSongModel model)
        {
            _songService.Add(model);
        }

        [HttpPut]
        [CustomAuthorize(true)]
        public void Put([FromUri] string id, [FromBody] AddUpdateSongModel model)
        {
            _songService.Update(id, model);
        }

        [HttpDelete]
        [CustomAuthorize(true)]
        public void Delete([FromUri] string id)
        {
            _songService.Delete(id);
        }

        [HttpGet]
        [Route("api/songs/{id}/preference")]
        [CustomAuthorize]
        public bool Preference([FromUri] string id)
        {
            return _songService.GetUserSongPreference(id, RequestExtractor.GetToken(Request));
        }

        [HttpPost]
        [Route("api/songs/{id}/preference")]
        [CustomAuthorize]
        public bool TogglePreference([FromUri] string id)
        {
            return _songService.ToggleUserSongPreference(id, RequestExtractor.GetToken(Request));
        }

        [HttpGet]
        [Route("api/songs/{id}/recommendations")]
        [CustomAuthorize]
        public IEnumerable<SongRecommendationModel> GetRecommendations([FromUri] string id, int count)
        {
            return _songService.GetSongRecommendations(id, RequestExtractor.GetToken(Request), count);
        }
    }
}