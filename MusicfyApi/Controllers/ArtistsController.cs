using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;

namespace MusicfyApi.Controllers
{
    public class ArtistsController : ApiController
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public ArtistModel Get([FromUri] int id)
        {
            return _artistService.GetById(id);
        }
    }
}