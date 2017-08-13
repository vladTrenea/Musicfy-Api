using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;

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
        public IEnumerable<SongCategoryModel> Get()
        {
            return _songCategoryService.GetAll();
        }

        [HttpGet]
        [Route("api/songs/categories/{id}")]
        public SongCategoryModel Get([FromUri] string id)
        {
            return _songCategoryService.GetById(id);
        }

        [HttpPost]
        [Route("api/songs/categories")]
        public void Post([FromBody] SongCategoryModel songCategoryModel)
        {
            _songCategoryService.Add(songCategoryModel);
        }

        [HttpPut]
        [Route("api/songs/categories/{id}")]
        public void Put([FromUri] string id, [FromBody] SongCategoryModel songCategoryModel)
        {
            _songCategoryService.Update(id, songCategoryModel);
        }

        [HttpDelete]
        [Route("api/songs/categories/{id}")]
        public void Delete([FromUri] string id)
        {
            _songCategoryService.Delete(id);
        }
    }
}