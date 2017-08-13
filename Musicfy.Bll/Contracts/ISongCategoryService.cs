using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface ISongCategoryService
    {
        IEnumerable<SongCategoryModel> GetAll();
    }
}