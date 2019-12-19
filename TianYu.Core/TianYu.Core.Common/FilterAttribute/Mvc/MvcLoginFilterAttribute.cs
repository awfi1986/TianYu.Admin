using TianYu.Core.Cache;
using TianYu.Core.Common.BaseViewModel;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace TianYu.Core.Common.FilterAttribute.Mvc
{
    public class MvcLoginFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(MvcIgnoreLoginAttribute), true).Any() || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(MvcIgnoreLoginAttribute), true).Any())
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            //未登录时，跳转到登录
            var cookie = CookieHelper.GetCookieValue(TianYuConsts.SystemLoginCookieName);

            var loginInfoCacheKey = TianYuConsts.GetLoginUserInfoCacheKey(cookie);

            var loginInfo = CacheHelper.Get<SystemLoginUserInfo>(loginInfoCacheKey);

            if (loginInfo == null)
            {
                var loginUrl = "/Home/Login";

                var request = filterContext.RequestContext.HttpContext.Request;
                //取消回调地址，因使用首页框架内嵌页面，此处回调只能回调首页
                //var returnUrl = request.Url.OriginalString;
                var returnUrl = string.Format("{0}://{1}/Home/Index", request.Url.Scheme, request.Url.Authority);

                var redirect = new RedirectResult(string.Format("{0}?redirectUrl={1}", loginUrl,
                    WebUtility.UrlEncode(returnUrl)));
                filterContext.Result = redirect;
                return;
            }

            base.OnActionExecuting(filterContext);
            return;
        }
    }
}
