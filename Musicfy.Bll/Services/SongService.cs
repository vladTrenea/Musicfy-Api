using System.Linq;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Mappers;
using Musicfy.Bll.Models;
using Musicfy.Bll.Utils;
using Musicfy.Dal.Contracts;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;
using Musicfy.Infrastructure.Utils;

namespace Musicfy.Bll.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        private readonly IArtistRepository _artistRepository;

        private readonly ISongCategoryRepository _songCategoryRepository;

        public SongService(ISongRepository songRepository, IArtistRepository artistRepository, ISongCategoryRepository songCategoryRepository)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _songCategoryRepository = songCategoryRepository;
        }

        public SongModel GetById(string id)
        {
            var song = _songRepository.GetById(id);
            if (song == null)
            {
                throw new NotFoundException(Messages.InvalidSongId);
            }

            var songModel = SongMapper.ToSongModel(song);

            return songModel;
        }

        public PaginationModel<SongItemModel> GetPaginated(int pageNumber, int count)
        {
            if (pageNumber < 1 || count < 0)
            {
                throw new ValidationException(Messages.InvalidPaginationInput);
            }

            var totalCount = _songRepository.GetTotalCount();
            var artists = _songRepository.GetPaginated(pageNumber, count);
            var paginationModel = new PaginationModel<SongItemModel>()
            {
                Page = pageNumber,
                Items = artists.Select(SongMapper.ToSongItemModel),
                TotalPages = totalCount%count == 0 ? (totalCount/count) : (totalCount/count) + 1
            };

            return paginationModel;
        }

        public void Add(AddUpdateSongModel addSongModel)
        {
            if (string.IsNullOrEmpty(addSongModel.Name))
            {
                throw new ValidationException(Messages.SongNameRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.Url))
            {
                throw new ValidationException(Messages.SongUrlRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.SongCategoryId))
            {
                throw new ValidationException(Messages.SongCategoryIdRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.ArtistId))
            {
                throw new ValidationException(Messages.SongArtistIdRequired);
            }

            var songCategory = _songCategoryRepository.GetById(addSongModel.SongCategoryId);
            if (songCategory == null)
            {
                throw new NotFoundException(Messages.InvalidSongCategoryId);
            }

            var artist = _artistRepository.GetById(addSongModel.ArtistId);
            if (artist == null)
            {
                throw new NotFoundException(Messages.InvalidArtistId);
            }

            var song = SongMapper.ToSong(addSongModel);
            song.Id = SecurityUtils.GenerateEntityId();
            _songRepository.Add(song);
        }

        public void Update(string id, AddUpdateSongModel addSongModel)
        {
            if (string.IsNullOrEmpty(addSongModel.Name))
            {
                throw new ValidationException(Messages.SongNameRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.Url))
            {
                throw new ValidationException(Messages.SongUrlRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.SongCategoryId))
            {
                throw new ValidationException(Messages.SongCategoryIdRequired);
            }

            if (string.IsNullOrEmpty(addSongModel.ArtistId))
            {
                throw new ValidationException(Messages.SongArtistIdRequired);
            }

            var songCategory = _songCategoryRepository.GetById(addSongModel.SongCategoryId);
            if (songCategory == null)
            {
                throw new NotFoundException(Messages.InvalidSongCategoryId);
            }

            var artist = _artistRepository.GetById(addSongModel.ArtistId);
            if (artist == null)
            {
                throw new NotFoundException(Messages.InvalidArtistId);
            }

            var song = SongMapper.ToSong(addSongModel);
            _songRepository.Update(song);
        }

        public void Delete(string id)
        {
            var songDetails = _songRepository.GetById(id);
            if (songDetails == null)
            {
                throw new NotFoundException(Messages.InvalidSongId);
            }

            _songRepository.Delete(id);
        }

        public bool GetUserSongPreference(string songId, string userToken)
        {
            var userAuthorization = AuthorizationCache.Instance.GetByToken(userToken);

            var song = _songRepository.GetById(songId);
            if (song == null)
            {
                throw new NotFoundException(Messages.InvalidSongId);
            }

            var currentUser = song.Supporters.FirstOrDefault(s => s.Id == userAuthorization.Id);
            if (currentUser != null)
            {
                return true;
            }

            return false;
        }

        public bool ToggleUserSongPreference(string songId, string userToken)
        {
            var userAuthorization = AuthorizationCache.Instance.GetByToken(userToken);
            var isUserSupporter = false;

            var song = _songRepository.GetById(songId);
            if (song == null)
            {
                throw new NotFoundException(Messages.InvalidSongId);
            }

            var currentUser = song.Supporters.FirstOrDefault(s => s.Id == userAuthorization.Id);
            if (currentUser != null)
            {
                isUserSupporter = true;
            }

            _songRepository.ToggleLike(!isUserSupporter, userAuthorization.Id, songId);

            return !isUserSupporter;
        }
    }
}