using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface IArtistRepository
    {
        Artist GetById(string id);

        Artist GetByName(string name);

        IEnumerable<Artist> GetPaginated(int pageNumber, int count);

        void Add(Artist artist);

        void Update(Artist artist);

        void Delete(string id);
    }
}