using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();
        Artist GetById(string id);

        Artist GetByName(string name);

        IEnumerable<Artist> GetPaginated(int pageNumber, int count);

        int GetTotalCount();

        void Add(Artist artist);

        void Update(Artist artist);

        void Delete(string id);
    }
}