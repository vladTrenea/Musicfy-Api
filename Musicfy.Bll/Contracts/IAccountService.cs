using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IAccountService
    {
        UserAuthorizationModel Login(LoginModel loginModel);

        void Logout(string authToken);
    }
}