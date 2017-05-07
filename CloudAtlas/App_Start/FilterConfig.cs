using CloudAtlas.Handlers;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomHandlerErrorAttribute());
        }
    }
}
