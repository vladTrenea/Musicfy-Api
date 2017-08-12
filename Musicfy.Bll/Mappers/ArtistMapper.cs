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
                Name = artist.name,
                Description = artist.description
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
                id = artistModel.Id,
                name = artistModel.Name,
                description = artistModel.Description
            };

            return artist;
        } 
    }
}