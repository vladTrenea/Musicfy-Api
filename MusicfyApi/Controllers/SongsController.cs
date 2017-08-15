using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Infrastructure.Configs;

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
        public PaginationModel<SongItemModel> Get(int pageNumber)
        {
            return _songService.GetPaginated(pageNumber, Config.ArtistsPageCount);
        }

        [HttpGet]
        public SongModel Get([FromUri] string id)
        {
            return _songService.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] AddUpdateSongModel model)
        {
            _songService.Add(model);
        }

        [HttpPut]
        public void Put([FromUri] string id, [FromBody] AddUpdateSongModel model)
        {
            _songService.Update(id, model);
        }

        [HttpDelete]
        public void Delete([FromUri] string id)
        {
            _songService.Delete(id);
        }
    }
}