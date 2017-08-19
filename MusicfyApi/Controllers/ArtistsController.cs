using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Infrastructure.Configs;
using MusicfyApi.Attributes;

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
        [CustomAuthorize]
        public IEnumerable<ArtistModel> Get()
        {
            return _artistService.GetAll();
        }

        [HttpGet]
        [CustomAuthorize]
        public ArtistModel Get([FromUri] string id)
        {
            return _artistService.GetById(id);
        }

        [HttpGet]
        [CustomAuthorize]
        public PaginationModel<ArtistModel> Get(int pageNumber)
        {
            return _artistService.GetPaginated(pageNumber, Config.ArtistsPageCount);
        }

        [HttpPost]
        [CustomAuthorize(true)]
        public void Post([FromBody] ArtistModel artist)
        {
            _artistService.Add(artist);
        }

        [HttpPut]
        [CustomAuthorize(true)]
        public void Put([FromUri] string id, [FromBody] ArtistModel artist)
        {
            _artistService.Update(id, artist);
        }

        [HttpDelete]
        [CustomAuthorize(true)]
        public void Delete([FromUri] string id)
        {
            _artistService.Delete(id);
        }
    }
}