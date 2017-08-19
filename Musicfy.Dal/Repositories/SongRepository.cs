using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Dto;
using Musicfy.Dal.Entities;
using Neo4jClient;
using Neo4jClient.Transactions;

namespace Musicfy.Dal.Repositories
{
    public class SongRepository : BaseRepository, ISongRepository
    {
        public SongRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public IEnumerable<Song> GetPaginated(int pageNumber, int count)
        {
            return _graphClient.Cypher
                .Match("(song:Song)")
                .With("song")
                .OrderBy("song.title")
                .Skip((pageNumber - 1)*count)
                .Limit(count)
                .Return(song => song.As<Song>())
                .Results;
        }

        public int GetTotalCount()
        {
            return _graphClient.Cypher
                .Match("(song:Song)")
                .Return(song => song.As<Song>())
                .Results
                .Count();
        }

        public SongDetailsDto GetById(string id)
        {
            return _graphClient.Cypher
                .OptionalMatch("(song:Song)-[COMPOSED_BY]->(artist:Artist)")
                .Where((Song song) => song.Id == id)
                .OptionalMatch("(song:Song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .OptionalMatch("(song:Song)-[CONTAINS]->(instrument:Instrument)")
                .OptionalMatch("(song:Song)-[DESCRIBED_BY]->(tag:Tag)")
                .Return((song, artist, songCategory, instrument, tag) => new SongDetailsDto
                {
                    Song = song.As<Song>(),
                    Artist = artist.As<Artist>(),
                    SongCategory = songCategory.As<SongCategory>(),
                    Instruments = instrument.CollectAs<Instrument>(),
                    Tags = tag.CollectAs<Tag>()
                })
                .Results
                .FirstOrDefault();
        }

        public void Add(Song newSong)
        {
            var transactionClient = (ITransactionalGraphClient) _graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                _graphClient.Cypher
                    .Merge("(song:Song { Id: {id} })")
                    .OnCreate()
                    .Set("song = {newSong}")
                    .WithParams(new
                    {
                        id = newSong.Id,
                        newSong
                    })
                    .ExecuteWithoutResults();

                AddSongRelationships(newSong);
                transaction.Commit();
            }
        }

        public void Update(Song songToUpdate)
        {
            var transactionClient = (ITransactionalGraphClient) _graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                _graphClient.Cypher
                    .OptionalMatch("(song:Song)-[r]-(instrument:Instrument)")
                    .Where((Song song) => song.Id == songToUpdate.Id)
                    .Delete("r")
                    .ExecuteWithoutResults();

                _graphClient.Cypher
                    .OptionalMatch("(song:Song)-[r]-(tag:Tag)")
                    .Where((Song song) => song.Id == songToUpdate.Id)
                    .Delete("r")
                    .ExecuteWithoutResults();

                AddSongRelationships(songToUpdate);
                transaction.Commit();
            }
        }

        public void Delete(string id)
        {
            var transactionClient = (ITransactionalGraphClient) _graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                _graphClient.Cypher
                    .OptionalMatch("(song:Song)-[r]-()")
                    .Where((Song song) => song.Id == id)
                    .Delete("r, song")
                    .ExecuteWithoutResults();

                _graphClient.Cypher
                    .Match("(song:Song)")
                    .Where((Song song) => song.Id == id)
                    .Delete("song")
                    .ExecuteWithoutResults();
                transaction.Commit();
            }
        }

        private void AddSongRelationships(Song songEntity)
        {
            _graphClient.Cypher
                .Match("(song:Song)", "(artist:Artist)")
                .Where((Artist artist) => artist.Id == songEntity.Artist.Id)
                .AndWhere((Song song) => song.Id == songEntity.Id)
                .CreateUnique("(song)-[:COMPOSED_BY]->(artist)")
                .ExecuteWithoutResults();

            foreach (var songInstrument in songEntity.Instruments)
            {
                _graphClient.Cypher
                    .Match("(song:Song)", "(instrument:Instrument)")
                    .Where((Instrument instrument) => instrument.Id == songInstrument.Id)
                    .AndWhere((Song song) => song.Id == songEntity.Id)
                    .CreateUnique("(song)-[:CONTAINS]->(instrument)")
                    .ExecuteWithoutResults();
            }

            _graphClient.Cypher
                .Match("(song:Song)", "(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Id == songEntity.SongCategory.Id)
                .AndWhere((Song song) => song.Id == songEntity.Id)
                .CreateUnique("(song)-[:CATEGORIZED_BY]->(songCategory)")
                .ExecuteWithoutResults();

            foreach (var songTag in songEntity.Tags)
            {
                _graphClient.Cypher
                    .Match("(song:Song)", "(tag:Tag)")
                    .Where((Song song) => song.Id == songEntity.Id)
                    .AndWhere((Tag tag) => tag.Id == songTag.Id)
                    .CreateUnique("(song)-[:DESCRIBED_BY]->(tag)")
                    .ExecuteWithoutResults();
            }
        }
    }
}