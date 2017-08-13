﻿using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;

namespace Musicfy.Dal.Repositories
{
    public class ArtistRepository : BaseRepository, IArtistRepository
    {
        public ArtistRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public Artist GetById(string id)
        {
            return _graphClient.Cypher
                .Match("(artist:Artist)")
                .Where((Artist artist) => artist.Id == id)
                .Return(artist => artist.As<Artist>())
                .Results
                .FirstOrDefault();
        }

        public Artist GetByName(string name)
        {
            return _graphClient.Cypher
                .Match("(artist:Artist)")
                .Where((Artist artist) => artist.Name == name)
                .Return(artist => artist.As<Artist>())
                .Results
                .FirstOrDefault();
        }

        public IEnumerable<Artist> GetPaginated(int pageNumber, int count)
        {
            return _graphClient.Cypher.Match("(artist:Artist)")
                //                    .Skip((pageNumber - 1) * count)
                //                    .Limit(count)
                //                    .OrderBy("name")
                .Return(artist => artist.As<Artist>())
                .Results;
        }

        public void Add(Artist artist)
        {
            _graphClient.Cypher
                .Create("(artist:Artist {newArtist})")
                .WithParam("newArtist", artist)
                .ExecuteWithoutResults();
        }

        public void Update(Artist updatedArtist)
        {
            _graphClient.Cypher
                .Match("(artist:Artist)")
                .Where((Artist artist) => artist.Id == updatedArtist.Id)
                .Set("artist = {updatedArtist}")
                .WithParam("updatedArtist",
                    new Artist {Id = updatedArtist.Id, Name = updatedArtist.Name, Description = updatedArtist.Description})
                .ExecuteWithoutResults();
        }

        public void Delete(string id)
        {
            _graphClient.Cypher
                .Match("(artist:Artist)")
                .Where((Artist artist) => artist.Id == id)
                .Delete("artist")
                .ExecuteWithoutResults();
        }
    }
}