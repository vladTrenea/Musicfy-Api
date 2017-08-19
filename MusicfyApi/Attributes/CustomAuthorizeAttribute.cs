using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;
using Musicfy.Bll.Contracts;
using Musicfy.DI;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;
using MusicfyApi.Utils;

namespace MusicfyApi.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _requiresAdmin;

        public CustomAuthorizeAttribute()
        {
            _requiresAdmin = false;
        }
        public CustomAuthorizeAttribute(bool requiresAdmin)
        {
            _requiresAdmin = requiresAdmin;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var container = Bootstrapper.Container;
            var authenticationService = container.Resolve<IAccountService>();

            var token = RequestExtractor.GetToken(actionContext.Request);
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedException(Messages.Forbidden);
            }

            var authorization = authenticationService.GetUserAuthorization(token);
            if (authorization == null)
            {
                throw new UnauthorizedException(Messages.Forbidden);
            }

            if (!authorization.IsAdmin && _requiresAdmin)
            {
                throw new ForbiddenException(Messages.Forbidden);
            }
        }
    }
}