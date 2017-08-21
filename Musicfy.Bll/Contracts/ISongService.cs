using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface ISongService
    {
        SongModel GetById(string id);

        PaginationModel<SongItemModel> GetPaginated(int pageNumber, int count);

        void Add(AddUpdateSongModel model);

        void Update(string id, AddUpdateSongModel model);

        void Delete(string id);

        bool GetUserSongPreference(string songId, string userToken);

        bool ToggleUserSongPreference(string songId, string userToken);

        IEnumerable<SongItemModel> GetSongRecommendations(string songId, string userToken, int maxCount);

        SongsDiscoverModel DiscoverRelatedSongs(string name, string userToken, int maxCount);
    }
}