using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface ISongService
    {
        SongModel GetById(string id);

        PaginationModel<SongItemModel> GetPaginated(int pageNumber, int count);

        void Add(AddSongModel addSongModel);

        void Delete(string id);
    }
}