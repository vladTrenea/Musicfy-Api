using System.Collections.Generic;
using Musicfy.Dal.Dto;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface ISongRepository
    {
        IEnumerable<SongDetailsDto> GetPaginated(int pageNumber, int count);

        int GetTotalCount();

        SongDetailsDto GetById(string id);

        void Add(Song song);

        void Update(Song song);

        void Delete(string id);

        void ToggleLike(bool likes, string userId, string songId);

        SongRecommendationResultDto GetSimilarById(string songId, string userId, int maxCount);

        SongRecommendationResultDto GetSimilarByTitle(string title, string userId, int maxCount);
    }
}