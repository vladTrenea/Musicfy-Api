using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Dto
{
    public class SongRecommendationDto
    {
        public Song Song { get; set; }

        public Artist Artist { get; set; }

        public long CommonInstruments { get; set; }

        public long CommonTags { get; set; }

        public long NumberOfLikes { get; set; }

        public long RecommendationScore { get; set; }
    }
}