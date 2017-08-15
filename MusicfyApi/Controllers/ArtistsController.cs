using System.Collections.Generic;
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
        public IEnumerable<ArtistModel> Get()
        {
            return _artistService.GetAll();
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

        [HttpPost]
        public void Post([FromBody] ArtistModel artist)
        {
            _artistService.Add(artist);
        }

        [HttpPut]
        public void Put([FromUri] string id, [FromBody] ArtistModel artist)
        {
            _artistService.Update(id, artist);
        }

        [HttpDelete]
        public void Delete([FromUri] string id)
        {
            _artistService.Delete(id);
        }
    }
}