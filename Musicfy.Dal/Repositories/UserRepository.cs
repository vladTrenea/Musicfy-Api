﻿using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;

namespace Musicfy.Dal.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public User GetById(string id)
        {
            return _graphClient.Cypher.Match("(user:User)")
                .Where((User user) => user.Id == id)
                .Return(user => user.As<User>())
                .Results.FirstOrDefault();
        }

        public User GetByCredentials(string username, string password)
        {
            return _graphClient.Cypher.Match("(user:User)")
                .Where((User user) => user.Username == username && user.Password == password)
                .Return(user => user.As<User>())
                .Results.FirstOrDefault();
        }
    }
}