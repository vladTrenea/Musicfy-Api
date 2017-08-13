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
    }
}