using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;
using Neo4jClient.Transactions;

namespace Musicfy.Dal.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public IEnumerable<Tag> GetAll()
        {
            return _graphClient.Cypher
                .Match("(tag:Tag)")
                .With("tag")
                .OrderBy("tag.name")
                .Return(tag => tag.As<Tag>())
                .Results;
        }

        public Tag GetById(string id)
        {
            return _graphClient.Cypher
                .Match("(tag:Tag)")
                .Where((Tag tag) => tag.Id == id)
                .Return(tag => tag.As<Tag>())
                .Results
                .FirstOrDefault();
        }

        public Tag GetByName(string name)
        {
            return _graphClient.Cypher
                .Match("(tag:Tag)")
                .Where((Tag tag) => tag.Name == name)
                .Return(tag => tag.As<Tag>())
                .Results
                .FirstOrDefault();
        }

        public void Add(Tag tag)
        {
            _graphClient.Cypher
                .Create("(tag:Tag {newTag})")
                .WithParam("newTag", tag)
                .ExecuteWithoutResults();
        }

        public void Update(Tag updatedTag)
        {
            _graphClient.Cypher
                .Match("(tag:Tag)")
                .Where((Tag tag) => tag.Id == updatedTag.Id)
                .Set("tag.name = {newName}")
                .WithParam("newName", updatedTag.Name)
                .ExecuteWithoutResults();
        }

        public void Delete(string id)
        {
            var transactionClient = (ITransactionalGraphClient)_graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                _graphClient.Cypher
                    .OptionalMatch("(tag:Tag)-[r]-()")
                    .Where((Tag tag) => tag.Id == id)
                    .Delete("r, tag")
                    .ExecuteWithoutResults();

                _graphClient.Cypher
                    .Match("(tag:Tag)")
                    .Where((Tag tag) => tag.Id == id)
                    .Delete("tag")
                    .ExecuteWithoutResults();

                transaction.Commit();
            }
        }
    }
}