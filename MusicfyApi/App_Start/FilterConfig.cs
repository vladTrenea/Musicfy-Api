using System.Web.Mvc;
using MusicfyApi.Attributes;

namespace MusicfyApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
    }
}