using System.Collections.Generic;
using System.Linq;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Mappers;
using Musicfy.Bll.Models;
using Musicfy.Dal.Contracts;

namespace Musicfy.Bll.Services
{
    public class SongCategoryService : ISongCategoryService
    {
        private readonly ISongCategoryRepository _songCategoryRepository;

        public SongCategoryService(ISongCategoryRepository songCategoryRepository)
        {
            _songCategoryRepository = songCategoryRepository;
        }

        public IEnumerable<SongCategoryModel> GetAll()
        {
            var songCategories = _songCategoryRepository.GetAll();

            return songCategories.Select(SongCategoryMapper.ToSongCategoryModel);
        }
    }
}