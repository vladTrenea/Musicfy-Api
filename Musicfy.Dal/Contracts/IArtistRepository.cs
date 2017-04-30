using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface IArtistRepository
    {
        Artist GetById(string id);

        IEnumerable<Artist> GetAll();
    }
}