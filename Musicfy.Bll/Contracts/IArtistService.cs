using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IArtistService
    {
        ArtistModel GetById(string id);

        IEnumerable<ArtistModel> GetAll();

        PaginationModel<ArtistModel> GetPaginated(int pageNumber, int count);
    }
}