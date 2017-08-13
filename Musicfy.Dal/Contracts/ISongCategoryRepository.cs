using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface ISongCategoryRepository
    {
        IEnumerable<SongCategory> GetAll();

        SongCategory GetById(string id);
    }
}