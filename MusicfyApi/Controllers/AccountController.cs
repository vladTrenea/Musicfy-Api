using System.Web.Http;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using MusicfyApi.Utils;

namespace MusicfyApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("api/account/login")]
        public UserAuthorizationModel Login([FromBody] LoginModel model)
        {
            return _accountService.Login(model);
        }

        [HttpPost]
        [Route("api/account/logout")]
        public void Logout()
        {
            _accountService.Logout(RequestExtractor.GetToken(Request));
        }
    }
}