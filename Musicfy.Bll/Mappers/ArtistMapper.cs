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
                Id = artist.Id,
                Name = artist.Name,
                Description = artist.Description
            };

            return artistModel;
        }

        public static Artist ToArtist(ArtistModel artistModel)
        {
            if (artistModel == null)
            {
                return null;
            }

            var artist = new Artist
            {
                Id = artistModel.Id,
                Name = artistModel.Name,
                Description = artistModel.Description
            };

            return artist;
        }

        public static void RefreshArtist(Artist artist, ArtistModel artistModel)
        {
            artist.Name = artistModel.Name;
            artist.Description = artistModel.Description;
        }
    }
}