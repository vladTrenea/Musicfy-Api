using System.Linq;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Mappers;
using Musicfy.Bll.Models;
using Musicfy.Dal.Contracts;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;
using Musicfy.Infrastructure.Utils;

namespace Musicfy.Bll.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public ArtistModel GetById(string id)
        {
            var artist = _artistRepository.GetById(id);
            if (artist == null)
            {
                throw new NotFoundException(Messages.InvalidArtistId);
            }

            return ArtistMapper.ToArtistModel(artist);
        }

        public PaginationModel<ArtistModel> GetPaginated(int pageNumber, int count)
        {
            if (pageNumber < 1 || count < 0)
            {
                throw new ValidationException(Messages.InvalidPaginationInput);
            }

            var artists = _artistRepository.GetPaginated(pageNumber, count);
            var paginationModel = new PaginationModel<ArtistModel>()
            {
                Page = pageNumber,
                Items = artists.Select(ArtistMapper.ToArtistModel),
                TotalPages = 1
            };

            return paginationModel;
        }

        public void Add(ArtistModel artistModel)
        {
            if (string.IsNullOrEmpty(artistModel.Name))
            {
                throw new ValidationException(Messages.ArtistNameRequired);
            }

            if (string.IsNullOrEmpty(artistModel.Description))
            {
                throw new ValidationException(Messages.ArtistDescriptionRequired);
            }

            var artistByName = _artistRepository.GetByName(artistModel.Name);
            if (artistByName != null)
            {
                throw new ConflictException(Messages.ArtistNameAlreadyExists);
            }

            var artist = ArtistMapper.ToArtist(artistModel);
            artist.Id = SecurityUtils.GenerateEntityId();

            _artistRepository.Add(artist);
        }

        public void Update(string id, ArtistModel artistModel)
        {
            if (string.IsNullOrEmpty(artistModel.Name))
            {
                throw new ValidationException(Messages.ArtistNameRequired);
            }

            if (string.IsNullOrEmpty(artistModel.Description))
            {
                throw new ValidationException(Messages.ArtistDescriptionRequired);
            }

            var artist = _artistRepository.GetById(id);
            if (artist == null)
            {
                throw new NotFoundException(Messages.InvalidArtistId);
            }

            var artistByName = _artistRepository.GetByName(artist.Name);
            if (artistByName != null && artistByName.Id != id)
            {
                throw new ConflictException(Messages.ArtistNameAlreadyExists);
            }

            ArtistMapper.RefreshArtist(artist, artistModel);
            _artistRepository.Update(artist);
        }

        public void Delete(string id)
        {
            var artist = _artistRepository.GetById(id);
            if (artist == null)
            {
                throw new NotFoundException(Messages.InvalidArtistId);
            }

            _artistRepository.Delete(id);
        }
    }
}