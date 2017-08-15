using Musicfy.Bll.Models;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class TagMapper
    {
        public static TagModel ToTagModel(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }

            return new TagModel
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static Tag ToTag(TagModel tagModel)
        {
            if (tagModel == null)
            {
                return null;
            }

            return new Tag
            {
                Id = tagModel.Id,
                Name = tagModel.Name
            };
        }

        public static void RefreshInstrument(Tag tag, TagModel tagModel)
        {
            tag.Name = tagModel.Name;
        }
    }
}