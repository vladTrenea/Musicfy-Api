﻿using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Dto;
using Musicfy.Dal.Entities;
using Musicfy.Infrastructure.Configs;
using Neo4jClient;
using Neo4jClient.Transactions;

namespace Musicfy.Dal.Repositories
{
    public class SongRepository : BaseRepository, ISongRepository
    {
        public SongRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public IEnumerable<SongDetailsDto> GetPaginated(int pageNumber, int count)
        {
            return _graphClient.Cypher
                .Match("(song:Song)")
                .Match("(song)-[COMPOSED_BY]->(artist:Artist)")
                .With("song, artist")
                .OrderBy("song.title")
                .Skip((pageNumber - 1)*count)
                .Limit(count)
                .Return((song, artist) => new SongDetailsDto
                {
                    Song = song.As<Song>(),
                    Artist = artist.As<Artist>()
                })
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
                .Match("(song:Song)-[COMPOSED_BY]->(artist:Artist)")
                .Where((Song song) => song.Id == id)
                .Match("(song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .OptionalMatch("(song)-[CONTAINS]->(instrument:Instrument)")
                .OptionalMatch("(song)-[DESCRIBED_BY]->(tag:Tag)")
                .OptionalMatch("(user:User)-[LIKES]->(song)")
                .Return((song, artist, songCategory, instrument, tag, user) => new SongDetailsDto
                {
                    Song = song.As<Song>(),
                    Artist = artist.As<Artist>(),
                    SongCategory = songCategory.As<SongCategory>(),
                    Instruments = instrument.CollectAs<Instrument>(),
                    Tags = tag.CollectAs<Tag>(),
                    Supporters = user.CollectAs<User>()
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

        public void ToggleLike(bool likes, string userId, string songId)
        {
            var transactionClient = (ITransactionalGraphClient) _graphClient;

            using (var transaction = transactionClient.BeginTransaction())
            {
                if (likes)
                {
                    _graphClient.Cypher
                        .Match("(user:User)", "(song:Song)")
                        .Where((User user) => user.Id == userId)
                        .AndWhere((Song song) => song.Id == songId)
                        .CreateUnique("(user)-[:LIKES]->(song)")
                        .ExecuteWithoutResults();
                }
                else
                {
                    _graphClient.Cypher
                        .OptionalMatch("(user:User)-[r]->(song:Song)")
                        .Where((Song song) => song.Id == songId)
                        .AndWhere((User user) => user.Id == userId)
                        .Delete("r")
                        .ExecuteWithoutResults();
                }
                transaction.Commit();
            }
        }

        public SongRecommendationResultDto GetSimilarById(string songId, string userId, int maxCount)
        {
            var currentSong = _graphClient.Cypher
                .Match("(song:Song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .Match("(song)-[COMPOSED_BY]->(artist:Artist)")
                .Where((Song song) => song.Id == songId)
                .Return((song, songCategory, artist) => new SongDetailsDto
                {
                    Song = song.As<Song>(),
                    SongCategory = songCategory.As<SongCategory>(),
                    Artist = artist.As<Artist>()
                })
                .Results
                .FirstOrDefault();

            var songRecommendationResult = new SongRecommendationResultDto();
            if (currentSong != null && currentSong.Song != null)
            {
                songRecommendationResult.RecommendedSongs = GetSimilar(currentSong, userId, maxCount);
                songRecommendationResult.MatchedSongId = currentSong.Song.Id;
                songRecommendationResult.MatchedSongTitle = currentSong.Song.Title;
                songRecommendationResult.MatchedSongArtist = currentSong.Artist;
            }

            return songRecommendationResult;
        }

        public SongRecommendationResultDto GetSimilarByTitle(string title, string userId, int maxCount)
        {
            var currentSong = _graphClient.Cypher
                .Match("(song:Song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .Match("(song)-[COMPOSED_BY]->(artist:Artist)")
                .Where((Song song) => song.Title == title)
                .Return((song, songCategory, artist) => new SongDetailsDto
                {
                    Song = song.As<Song>(),
                    SongCategory = songCategory.As<SongCategory>(),
                    Artist = artist.As<Artist>()
                })
                .Results
                .FirstOrDefault();

            var songRecommendationResult = new SongRecommendationResultDto();
            if (currentSong != null && currentSong.Song != null)
            {
                songRecommendationResult.RecommendedSongs = GetSimilar(currentSong, userId, maxCount);
                songRecommendationResult.MatchedSongId = currentSong.Song.Id;
                songRecommendationResult.MatchedSongTitle = currentSong.Song.Title;
                songRecommendationResult.MatchedSongArtist = currentSong.Artist;
            }

            return songRecommendationResult;
        }

        private IEnumerable<SongRecommendationDto> GetSimilar(SongDetailsDto currentSong, string userId, int maxCount)
        {
            var songRecommendations = _graphClient.Cypher
                .Match("(song:Song)-[CATEGORIZED_BY]->(songCategory:SongCategory)")
                .Where((SongCategory songCategory) => songCategory.Id == currentSong.SongCategory.Id)
                .AndWhere((Song song) => song.Id != currentSong.Song.Id)
                .Match("(song)-[COMPOSED_BY]->(artist:Artist)")
                .Match("(targetSong:Song)")
                .Where((Song targetSong) => targetSong.Id == currentSong.Song.Id)
                .OptionalMatch("(song)-[ri:CONTAINS]->(instrument:Instrument)<-[:CONTAINS]-(targetSong)")
                .OptionalMatch("(song)-[rt:DESCRIBED_BY]->(tag:Tag)<-[:DESCRIBED_BY]-(targetSong)")
                .OptionalMatch("(user:User)-[LIKES]->(song)")
                .Where((User user) => user.Id != userId)
                .ReturnDistinct((song, artist, instrument, tag, user) => new SongRecommendationDto
                {
                    Song = song.As<Song>(),
                    Artist = artist.As<Artist>(),
                    CommonInstruments = instrument.CountDistinct(),
                    CommonTags = tag.CountDistinct(),
                    NumberOfLikes = user.Count()
                })
                .Results
                .OrderByDescending(ComputeRecommendationScore)
                .Take(maxCount);

            return songRecommendations;
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

        private long ComputeRecommendationScore(SongRecommendationDto songRecommendationDto)
        {
            return songRecommendationDto.CommonInstruments*Config.InstrumentRecommendationImportance
                   + songRecommendationDto.CommonTags*Config.TagRecommendationImportance
                   + songRecommendationDto.NumberOfLikes*Config.LikeRecommendationImportance;
        }
    }
}