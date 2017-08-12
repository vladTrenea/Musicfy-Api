using Musicfy.Bll.Contracts;
using Musicfy.Bll.Models;
using Musicfy.Bll.Utils;
using Musicfy.Dal.Contracts;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;
using Musicfy.Infrastructure.Utils;

namespace Musicfy.Bll.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserAuthorizationModel Login(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username))
            {
                throw new ValidationException(Messages.UsernameRequired);
            }

            if (string.IsNullOrEmpty(loginModel.Password))
            {
                throw new ValidationException(Messages.PasswordRequired);
            }

            var hashedPass = HashUtils.EncodeString(loginModel.Password);
            var user = _userRepository.GetByCredentials(loginModel.Username, hashedPass);

            if (user == null)
            {
                throw new UnauthorizedException(Messages.InvalidLogin);
            }

            var authorization = new UserAuthorizationModel
            {
                Id = user.id,
                Username = user.username,
                IsAdmin = user.isAdmin,
                Token = SecurityUtils.GenerateToken()
            };

            AuthorizationCache.Instance.AddOrUpdateAuthorization(authorization);

            return authorization;
        }

        public void Logout(string authToken)
        {
            var authorization = AuthorizationCache.Instance.GetByToken(authToken);
            if (authorization == null)
            {
                throw new UnauthorizedException(Messages.Unauthorized);
            }

            AuthorizationCache.Instance.DeleteAuthorization(authToken);
        }
    }
}