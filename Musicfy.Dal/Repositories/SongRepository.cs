using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;

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

        public Song GetById(string id)
        {
            return _graphClient.Cypher
                .OptionalMatch("(song:Song)-[COMPOSED_BY]->(artist:Artist)")
                .OptionalMatch("(song:Song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .OptionalMatch("(song:Song)-[CONTAINS]->(instrument:Instrument)")
                .OptionalMatch("(song:Song)-[DESCRIBED_BY]->(tag:Tag)")
                .Where((Song song) => song.Id == id)
                .Return((song, artist, songCategory, instrument, tag) => new Song
                {
                    Id = song.As<Song>().Id,
                    Title = song.As<Song>().Title,
                    Url = song.As<Song>().Url,
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

            _graphClient.Cypher
                .Match("(song:Song)", "(artist:Artist)")
                .Where((Artist artist) => artist.Id == newSong.Artist.Id)
                .AndWhere((Song song) => song.Id == newSong.Id)
                .CreateUnique("(song)-[:COMPOSED_BY]->(artist)")
                .ExecuteWithoutResults();

            _graphClient.Cypher
                .Match("(song:Song)", "(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Id == newSong.SongCategory.Id)
                .AndWhere((Song song) => song.Id == newSong.Id)
                .CreateUnique("(song)-[:CATEGORIZED_BY]->(songCategory)")
                .ExecuteWithoutResults();

            foreach (var songInstrument in newSong.Instruments)
            {
                _graphClient.Cypher
                    .Match("(song:Song)", "(instrument:Instrument)")
                    .Where((Instrument instrument) => instrument.Id == songInstrument.Id)
                    .AndWhere((Song song) => song.Id == newSong.Id)
                    .CreateUnique("(song)-[:CONTAINS]->(instrument)")
                    .ExecuteWithoutResults();
            }

            foreach (var songTag in newSong.Tags)
            {
                _graphClient.Cypher
                    .Match("(song:Song)", "(tag:Tag)")
                    .Where((Tag tag) => tag.Id == songTag.Id)
                    .AndWhere((Song song) => song.Id == newSong.Id)
                    .CreateUnique("(song)-[:DESCRIBED_BY]->(tag)")
                    .ExecuteWithoutResults();
            }
        }

        public void Delete(string id)
        {
            _graphClient.Cypher
                .OptionalMatch("(song:Song)-[r]-()")
                .Where((Song song) => song.Id == id)
                .Delete("r, song")
                .ExecuteWithoutResults();
        }
    }
}