using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IArtistService
    {
        IEnumerable<ArtistModel> GetAll();
        ArtistModel GetById(string id);

        PaginationModel<ArtistModel> GetPaginated(int pageNumber, int count);

        void Add(ArtistModel artistModel);

        void Update(string id, ArtistModel artistModel);

        void Delete(string id);
    }
}