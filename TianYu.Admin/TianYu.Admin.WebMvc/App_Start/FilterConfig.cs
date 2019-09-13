using TianYu.Core.Common;
using TianYu.Core.Log.FilterAttribute;
using System.Web;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;

namespace TianYu.Admin.WebMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //Web MVC 过滤器
            filters.Add(new MvcActionFilterAttribute());
            filters.Add(new MvcExceptionFilterAttribute());
            filters.Add(new MvcLoginFilterAttribute());
        }
    }
}
