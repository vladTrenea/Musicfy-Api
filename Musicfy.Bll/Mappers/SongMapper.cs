using System.Linq;
using Musicfy.Bll.Models;
using Musicfy.Dal.Dto;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class SongMapper
    {
        public static SongItemModel ToSongItemModel(Song song)
        {
            if (song == null)
            {
                return null;
            }

            return new SongItemModel
            {
                Id = song.Id,
                Name = song.Title
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
                Tags = songDetails.Tags.Select(TagMapper.ToTagModel),
                Instruments = songDetails.Instruments.Select(InstrumentMapper.ToInstrumentModel)
            };
        }
    }
}