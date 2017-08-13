using System.Collections.Generic;
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

        public SongCategoryModel GetById(string id)
        {
            var songCategory = _songCategoryRepository.GetById(id);
            if (songCategory == null)
            {
                throw new NotFoundException(Messages.InvalidSongCategoryId);
            }

            return SongCategoryMapper.ToSongCategoryModel(songCategory);
        }

        public void Add(SongCategoryModel songCategoryModel)
        {
            if (string.IsNullOrEmpty(songCategoryModel.Name))
            {
                throw new ValidationException(Messages.SongCategoryNameRequired);
            }

            var instrumentByName = _songCategoryRepository.GetByName(songCategoryModel.Name);
            if (instrumentByName != null)
            {
                throw new ConflictException(Messages.SongCategoryNameAlreadyExists);
            }

            var songCategory = SongCategoryMapper.ToSongCategory(songCategoryModel);
            songCategory.Id = SecurityUtils.GenerateEntityId();

            _songCategoryRepository.Add(songCategory);
        }

        public void Update(string id, SongCategoryModel songCategoryModel)
        {
            if (string.IsNullOrEmpty(songCategoryModel.Name))
            {
                throw new ValidationException(Messages.SongCategoryNameRequired);
            }

            var songCategory = _songCategoryRepository.GetById(id);
            if (songCategory == null)
            {
                throw new NotFoundException(Messages.InvalidSongCategoryId);
            }

            var instrumentByName = _songCategoryRepository.GetByName(songCategory.Name);
            if (instrumentByName != null && instrumentByName.Id != id)
            {
                throw new ConflictException(Messages.SongCategoryNameAlreadyExists);
            }

            SongCategoryMapper.RefreshSongCategory(songCategory, songCategoryModel);
            _songCategoryRepository.Update(songCategory);
        }

        public void Delete(string id)
        {
            var songCategory = _songCategoryRepository.GetById(id);
            if (songCategory == null)
            {
                throw new NotFoundException(Messages.InvalidSongCategoryId);
            }

            _songCategoryRepository.Delete(id);
        }
    }
}