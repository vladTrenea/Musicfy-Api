using Musicfy.Bll.Models;
using Musicfy.Dal.Entities;

namespace Musicfy.Bll.Mappers
{
    public static class InstrumentMapper
    {
        public static InstrumentModel ToInstrumentModel(Instrument instrument)
        {
            if (instrument == null)
            {
                return null;
            }

            return new InstrumentModel
            {
                Id = instrument.Id,
                Name = instrument.Name
            };
        }
    }
}