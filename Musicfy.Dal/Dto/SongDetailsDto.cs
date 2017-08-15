using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Dto
{
    public class SongDetailsDto
    {
        public Song Song { get; set; }

        public Artist Artist { get; set; }

        public SongCategory SongCategory { get; set; }

        public IEnumerable<Instrument> Instruments { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}