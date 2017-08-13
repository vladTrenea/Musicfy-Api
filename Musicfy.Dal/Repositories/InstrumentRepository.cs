using System.Collections.Generic;
using System.Linq;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Entities;
using Neo4jClient;

namespace Musicfy.Dal.Repositories
{
    public class InstrumentRepository : BaseRepository, IInstrumentRepository
    {
        public InstrumentRepository(IGraphClient graphClient) : base(graphClient)
        {
        }

        public IEnumerable<Instrument> GetAll()
        {
            return _graphClient.Cypher
                .Match("(instrument:Instrument)")
                .Return(instrument => instrument.As<Instrument>())
                .Results;
        }

        public Instrument GetById(string id)
        {
            return _graphClient.Cypher
                .Match("(instrument:Instrument)")
                .Where((SongCategory instrument) => instrument.Id == id)
                .Return(instrument => instrument.As<Instrument>())
                .Results
                .FirstOrDefault();
        }
    }
}