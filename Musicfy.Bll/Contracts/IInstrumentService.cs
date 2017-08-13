using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IInstrumentService
    {
        IEnumerable<InstrumentModel> GetAll();

        InstrumentModel GetById(string id);

        void Add(InstrumentModel artistModel);

        void Update(string id, InstrumentModel artistModel);

        void Delete(string id);
    }
}