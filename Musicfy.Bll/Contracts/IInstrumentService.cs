using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IInstrumentService
    {
        IEnumerable<InstrumentModel> GetAll();

        InstrumentModel GetById(string id);

        void Add(InstrumentModel instrumentModel);

        void Update(string id, InstrumentModel instrumentModel);

        void Delete(string id);
    }
}