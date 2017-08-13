using System.Collections.Generic;
using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IInstrumentService
    {
        IEnumerable<InstrumentModel> GetAll();
    }
}