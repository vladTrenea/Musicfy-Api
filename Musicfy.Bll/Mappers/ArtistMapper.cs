using Musicfy.Bll.Models;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistModel ToArtistModel(Artist artist)
        {
            if (artist == null)
            {
                return null;
            }

            var artistModel = new ArtistModel
            {
                Id = artist.id,
                Name = artist.name
            };

            return artistModel;
        }
    }
}