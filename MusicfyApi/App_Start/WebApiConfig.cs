using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Practices.Unity.WebApi;
using Musicfy.DI;

namespace MusicfyApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Bootstrapper.Container);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}