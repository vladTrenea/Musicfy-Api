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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<TagModel> GetAll()
        {
            var tags = _tagRepository.GetAll();

            return tags.Select(TagMapper.ToTagModel);
        }

        public TagModel GetById(string id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                throw new NotFoundException(Messages.InvalidTagId);
            }

            return TagMapper.ToTagModel(tag);
        }

        public void Add(TagModel tagModel)
        {
            if (string.IsNullOrEmpty(tagModel.Name))
            {
                throw new ValidationException(Messages.TagNameRequired);
            }

            var tagByName = _tagRepository.GetByName(tagModel.Name);
            if (tagByName != null)
            {
                throw new ConflictException(Messages.TagNameAlreadyExists);
            }

            var tag = TagMapper.ToTag(tagModel);
            tag.Id = SecurityUtils.GenerateEntityId();

            _tagRepository.Add(tag);
        }

        public void Update(string id, TagModel tagModel)
        {
            if (string.IsNullOrEmpty(tagModel.Name))
            {
                throw new ValidationException(Messages.TagNameRequired);
            }

            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                throw new NotFoundException(Messages.InvalidTagId);
            }

            var tagByName = _tagRepository.GetByName(tag.Name);
            if (tagByName != null && tagByName.Id != id)
            {
                throw new ConflictException(Messages.TagNameAlreadyExists);
            }

            TagMapper.RefreshInstrument(tag, tagModel);
            _tagRepository.Update(tag);
        }

        public void Delete(string id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                throw new NotFoundException(Messages.InvalidTagId);
            }

            _tagRepository.Delete(id);
        }
    }
}