using Musicfy.Bll.Models;

namespace Musicfy.Bll.Contracts
{
    public interface IAccountService
    {
        UserAuthorizationModel Login(LoginModel loginModel);

        UserAuthorizationModel GetUserAuthorization(string userToken);

        void Logout(string authToken);
    }
}