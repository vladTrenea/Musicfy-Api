using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;
using Neo4jClient.Transactions;

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
                .With("songCategory")
                .OrderBy("songCategory.name")
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

        public SongCategory GetByName(string name)
        {
            return _graphClient.Cypher
                .Match("(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Name == name)
                .Return(songCategory => songCategory.As<SongCategory>())
                .Results
                .FirstOrDefault();
        }

        public void Add(SongCategory songCategory)
        {
            _graphClient.Cypher
                .Create("(songCategory:SongCategory {newSongCategory})")
                .WithParam("newSongCategory", songCategory)
                .ExecuteWithoutResults();
        }

        public void Update(SongCategory updatedSongCategory)
        {
            _graphClient.Cypher
                .Match("(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Id == updatedSongCategory.Id)
                .Set("songCategory.name = {newName}")
                .WithParam("newName", updatedSongCategory.Name)
                .ExecuteWithoutResults();
        }

        public void Delete(string id)
        {
            var transactionClient = (ITransactionalGraphClient) _graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                _graphClient.Cypher
                    .OptionalMatch("(songCategory:SongCategory)-[r]-()")
                    .Where((SongCategory songCategory) => songCategory.Id == id)
                    .Delete("r, songCategory")
                    .ExecuteWithoutResults();

                _graphClient.Cypher
                    .Match("(songCategory:SongCategory)")
                    .Where((SongCategory songCategory) => songCategory.Id == id)
                    .Delete("songCategory")
                    .ExecuteWithoutResults();
                transaction.Commit();
            }
        }
    }
}