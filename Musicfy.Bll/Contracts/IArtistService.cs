using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IArtistService
    {
        ArtistModel GetById(string id);

        PaginationModel<ArtistModel> GetPaginated(int pageNumber, int count);

        void Add(ArtistModel artistModel);
    }
}