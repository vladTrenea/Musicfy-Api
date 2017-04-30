using System.Collections;
using System.Collections.Generic;
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
            return _graphClient.Cypher.Match("(artist:Artist)")
                .Where((Artist artist) => artist.id == id)
                .Return(artist => artist.As<Artist>())
                .Results.FirstOrDefault();
        }

        public IEnumerable<Artist> GetAll()
        {
            return _graphClient.Cypher.Match("(artist:Artist)").Return(artist => artist.As<Artist>()).Results;
        }
    }
}