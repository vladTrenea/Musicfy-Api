using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IArtistService
    {
        ArtistModel GetById(int id);

        ArtistModel GetByName(string name);
    }
}