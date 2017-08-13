using System.Collections.Generic;
using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;

namespace MusicfyApi.Controllers
{
    public class InstrumentsController : ApiController
    {
        private readonly IInstrumentService _instrumentService;

        public InstrumentsController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HttpGet]
        public IEnumerable<InstrumentModel> Get()
        {
            return _instrumentService.GetAll();
        }
    }
}