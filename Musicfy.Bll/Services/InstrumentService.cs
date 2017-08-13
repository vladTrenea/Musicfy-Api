using System.Collections.Generic;
using System.Linq;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Mappers;
using Musicfy.Bll.Models;
using Musicfy.Dal.Contracts;

namespace Musicfy.Bll.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;

        public InstrumentService(IInstrumentRepository instrumentRepository)
        {
            _instrumentRepository = instrumentRepository;
        }

        public IEnumerable<InstrumentModel> GetAll()
        {
            var instruments = _instrumentRepository.GetAll();

            return instruments.Select(InstrumentMapper.ToInstrumentModel);
        }
    }
}