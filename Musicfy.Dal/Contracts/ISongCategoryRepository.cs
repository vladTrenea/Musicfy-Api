using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface ISongCategoryRepository
    {
        IEnumerable<SongCategory> GetAll();

        SongCategory GetById(string id);

        SongCategory GetByName(string name);

        void Add(SongCategory songCategory);

        void Update(SongCategory songCategory);

        void Delete(string id);
    }
}