using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface ISongCategoryService
    {
        IEnumerable<SongCategoryModel> GetAll();

        SongCategoryModel GetById(string id);

        void Add(SongCategoryModel songCategoryModel);

        void Update(string id, SongCategoryModel songCategoryModel);

        void Delete(string id);
    }
}