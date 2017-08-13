using Musicfy.Bll.Models;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class SongCategoryMapper
    {
        public static SongCategoryModel ToSongCategoryModel(SongCategory songCategory)
        {
            if (songCategory == null)
            {
                return null;
            }

            return new SongCategoryModel
            {
                Id = songCategory.Id,
                Name = songCategory.Name
            };
        }

        public static SongCategory ToSongCategory(SongCategoryModel songCategoryModel)
        {
            if (songCategoryModel == null)
            {
                return null;
            }

            return new SongCategory
            {
                Id = songCategoryModel.Id,
                Name = songCategoryModel.Name
            };
        }

        public static void RefreshSongCategory(SongCategory songCategory, SongCategoryModel songCategoryModel)
        {
            songCategory.Name = songCategoryModel.Name;
        }
    }
}