using System.Collections.Generic;
using System.Linq;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Mappers;
using Musicfy.Bll.Models;
using Musicfy.Dal.Contracts;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;

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

        public IEnumerable<ArtistModel> GetAll()
        {
            var artists = _artistRepository.GetAll();

            return artists.Select(ArtistMapper.ToArtistModel);
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
                Items = artists.Select(ArtistMapper.ToArtistModel)
            };

            return paginationModel;
        }
    }
}