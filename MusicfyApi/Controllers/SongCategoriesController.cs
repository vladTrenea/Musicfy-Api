using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using MusicfyApi.Attributes;

namespace MusicfyApi.Controllers
{
    public class SongCategoriesController : ApiController
    {
        private readonly ISongCategoryService _songCategoryService;

        public SongCategoriesController(ISongCategoryService songCategoryService)
        {
            _songCategoryService = songCategoryService;
        }

        [HttpGet]
        [Route("api/songs/categories")]
        [CustomAuthorize]
        public IEnumerable<SongCategoryModel> Get()
        {
            return _songCategoryService.GetAll();
        }

        [HttpGet]
        [Route("api/songs/categories/{id}")]
        [CustomAuthorize]
        public SongCategoryModel Get([FromUri] string id)
        {
            return _songCategoryService.GetById(id);
        }

        [HttpPost]
        [Route("api/songs/categories")]
        [CustomAuthorize(true)]
        public void Post([FromBody] SongCategoryModel songCategoryModel)
        {
            _songCategoryService.Add(songCategoryModel);
        }

        [HttpPut]
        [Route("api/songs/categories/{id}")]
        [CustomAuthorize(true)]
        public void Put([FromUri] string id, [FromBody] SongCategoryModel songCategoryModel)
        {
            _songCategoryService.Update(id, songCategoryModel);
        }

        [HttpDelete]
        [Route("api/songs/categories/{id}")]
        [CustomAuthorize(true)]
        public void Delete([FromUri] string id)
        {
            _songCategoryService.Delete(id);
        }
    }
}