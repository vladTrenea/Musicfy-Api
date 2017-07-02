using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Infrastructure.Configs;

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
        public ArtistModel Get([FromUri] string id)
        {
            return _artistService.GetById(id);
        }

        [HttpGet]
        public PaginationModel<ArtistModel> Get(int pageNumber)
        {
            return _artistService.GetPaginated(pageNumber, Config.ArtistsPageCount);
        }
    }
}