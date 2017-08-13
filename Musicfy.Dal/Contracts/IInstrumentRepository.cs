using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface IInstrumentRepository
    {
        IEnumerable<Instrument> GetAll();

        Instrument GetById(string id);
    }
}