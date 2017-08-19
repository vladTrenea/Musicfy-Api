using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Infrastructure.Configs;
using MusicfyApi.Attributes;

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
            return _songService.GetPaginated(pageNumber, Config.ArtistsPageCount);
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
    }
}