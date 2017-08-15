using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;

namespace MusicfyApi.Controllers
{
    public class TagsController : ApiController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            return _tagService.GetAll();
        }

        [HttpGet]
        public TagModel Get([FromUri] string id)
        {
            return _tagService.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] TagModel tagModel)
        {
            _tagService.Add(tagModel);
        }

        [HttpPut]
        public void Put([FromUri] string id, [FromBody] TagModel tagModel)
        {
            _tagService.Update(id, tagModel);
        }

        [HttpDelete]
        public void Delete([FromUri] string id)
        {
            _tagService.Delete(id);
        }
    }
}