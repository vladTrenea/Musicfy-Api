using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Dto
{
    public class SongRecommendationResultDto
    {
        public string MatchedSongId { get; set; }

        public string MatchedSongTitle { get; set; }

        public Artist MatchedSongArtist { get; set; }

        public IEnumerable<SongRecommendationDto> RecommendedSongs { get; set; }

        public SongRecommendationResultDto()
        {
            this.RecommendedSongs = new List<SongRecommendationDto>();
        }
    }
}