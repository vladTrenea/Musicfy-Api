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
    }
}