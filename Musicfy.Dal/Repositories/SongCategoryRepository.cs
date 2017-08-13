using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;

namespace Musicfy.Dal.Repositories
{
    public class SongCategoryRepository : BaseRepository, ISongCategoryRepository
    {
        public SongCategoryRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public IEnumerable<SongCategory> GetAll()
        {
            return _graphClient.Cypher
                .Match("(songCategory:SongCategory)")
                .Return(songCategory => songCategory.As<SongCategory>())
                .Results;
        }

        public SongCategory GetById(string id)
        {
            return _graphClient.Cypher
                .Match("(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Id == id)
                .Return(songCategory => songCategory.As<SongCategory>())
                .Results
                .FirstOrDefault();
        }
    }
}