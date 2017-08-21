using System.Linq;
using Musicfy.Bll.Models;
using Musicfy.Dal.Dto;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class SongMapper
    {
        public static SongItemModel ToSongItemModel(SongDetailsDto songDetails)
        {
            if (songDetails == null)
            {
                return null;
            }

            return new SongItemModel
            {
                Id = songDetails.Song.Id,
                Name = songDetails.Song.Title,
                Artist = ArtistMapper.ToArtistModel(songDetails.Artist)
            };
        }

        public static Song ToSong(SongModel songModel)
        {
            if (songModel == null)
            {
                return null;
            }

            return new Song
            {
                Id = songModel.Id,
                Title = songModel.Name,
                Url = songModel.Url,
                Artist = ArtistMapper.ToArtist(songModel.Artist),
                SongCategory = SongCategoryMapper.ToSongCategory(songModel.SongCategory),
                Instruments = songModel.Instruments.Select(InstrumentMapper.ToInstrument),
                Tags = songModel.Tags.Select(TagMapper.ToTag)
            };
        }

        public static Song ToSong(AddUpdateSongModel addSongModel)
        {
            if (addSongModel == null)
            {
                return null;
            }

            var song = new Song
            {
                Id = addSongModel.Id,
                Title = addSongModel.Name,
                Url = addSongModel.Url,
                Artist = new Artist {Id = addSongModel.ArtistId},
                SongCategory = new SongCategory {Id = addSongModel.SongCategoryId},
                Instruments = addSongModel.InstrumentIds.Select(i => new Instrument {Id = i}),
                Tags = addSongModel.TagIds.Select(t => new Tag {Id = t})
            };

            return song;
        }

        public static SongModel ToSongModel(SongDetailsDto songDetails)
        {
            if (songDetails == null)
            {
                return null;
            }

            return new SongModel
            {
                Id = songDetails.Song.Id,
                Name = songDetails.Song.Title,
                Url = songDetails.Song.Url,
                Artist = ArtistMapper.ToArtistModel(songDetails.Artist),
                SongCategory = SongCategoryMapper.ToSongCategoryModel(songDetails.SongCategory),
                //TODO: Have a look over here
                Tags = songDetails.Tags.GroupBy(t => t.Id).Select(grp => TagMapper.ToTagModel(grp.FirstOrDefault())),
                Instruments = songDetails.Instruments.GroupBy(i => i.Id).Select(grp => InstrumentMapper.ToInstrumentModel(grp.FirstOrDefault()))
            };
        }
    }
}