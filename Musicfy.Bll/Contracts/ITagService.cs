using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface ITagService
    {
        IEnumerable<TagModel> GetAll();

        TagModel GetById(string id);

        void Add(TagModel tagModel);

        void Update(string id, TagModel tagModel);

        void Delete(string id);
    }
}