using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetPaginated(int pageNumber, int count);

        int GetTotalCount();

        Song GetById(string id);

        void Add(Song songDetails);

        void Delete(string id);
    }
}