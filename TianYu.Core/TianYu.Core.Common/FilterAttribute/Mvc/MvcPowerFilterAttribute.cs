
using TianYu.Core.Cache;
using TianYu.Core.Common.BaseViewModel;
using System; 
using System.Linq;
using System.Net; 
using System.Web;
using System.Web.Mvc;

namespace TianYu.Core.Common.FilterAttribute.Mvc
{
    public class MvcPowerFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(MvcIgnorePowerAttribute), true).Any() || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(MvcIgnorePowerAttribute), true).Any())
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            string areaName = filterContext.RouteData.DataTokens["area"].IsNull() ? "" : filterContext.RouteData.DataTokens["area"].ToString();
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            var actionUrl = string.Format("{0}/{1}/{2}", areaName, controllerName, actionName);
            ////未登录时，跳转到登录
            var currCookie = CookieHelper.GetCookieValue(TianYuConsts.SystemLoginCookieName);

            var loginInfoCacheKey = TianYuConsts.GetLoginUserInfoCacheKey(currCookie);


            //if (token.IsNullOrWhiteSpace())
            //{
            //    var loginUrl = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemManagerLoginUrl);
            //    var request = filterContext.RequestContext.HttpContext.Request;
            //    var redirect = new RedirectResult(string.Format("{0}?redirectUrl={1}", loginUrl,
            //        WebUtility.UrlEncode(request.Url.OriginalString)));
            //    filterContext.Result = redirect;
            //    return;
            //}
            var loginInfo = CacheHelper.Get<SystemLoginUserInfo>(loginInfoCacheKey);
            //if (userGroup == null)
            //{
            //    var loginUrl = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemManagerLoginUrl);
            //    var request = filterContext.RequestContext.HttpContext.Request;
            //    var redirect = new RedirectResult(string.Format("{0}?redirectUrl={1}", loginUrl,
            //        WebUtility.UrlEncode(request.Url.OriginalString)));
            //    filterContext.Result = redirect;
            //    return;
            //}
            if (loginInfo == null)
            {
                throw new Exception("没有找到登录用户信息");
            }

            //延长cookie时间
            //      CookieHelper.AppendCookieTime(TianYuConsts.SystemLoginCookieName, 24);

            var cookie = HttpContext.Current.Request.Cookies[TianYuConsts.SystemLoginCookieName];
            cookie.Domain = ConfigHelper.GetAppsettingValue("CookieDomainName");
            cookie.Expires = DateTime.Now.AddHours(24);
            cookie.HttpOnly = false;
            cookie.Path = "/";
            filterContext.HttpContext.Response.Cookies.Add(cookie);

            if (IsPowerAction(actionUrl, loginInfo))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                var jsonResult = new JsonResult();
                var result = new BaseResponse()
                {
                    Status = HttpStatusCode.Unauthorized,
                    ErrorMessage = "您没有访问权限"
                };
                jsonResult.Data = result;
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = jsonResult;
                }
                else
                {

                    var redirect = new RedirectResult("/Error/index?msg=" + result.ErrorMessage);
                    filterContext.Result = redirect;
                }


                return;
            }
        }
        /// <summary>
        /// 是否具备使用权限
        /// </summary>
        /// <param name="actionUrl">action地址</param>
        /// <param name="userGroupView"></param>
        /// <returns></returns>
        private bool IsPowerAction(string actionUrl, SystemLoginUserInfo userGroupView)
        {
            //主页、欢迎页，默认都有权限
            if(actionUrl.Equals("/Home/Index", StringComparison.InvariantCultureIgnoreCase) || actionUrl.Equals("/Home/Wecome", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            string bussionModule = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemManagerModuleName);
            if (bussionModule.IsNullOrWhiteSpace())
            {
                throw new Exception("未配置当前项目模块，请修改配置未见或增加cookie数据，配置键名：" + TianYuConsts.SystemManagerModuleName);
            }

            //var powerList = userGroupView.Privileges.Where(x => (x.Level == 3 || x.Level == 4));

            //bool flag = powerList.Any(t => !t.ActionUrl.IsNullOrWhiteSpace() && t.ActionUrl.EndsWith(actionUrl, StringComparison.InvariantCultureIgnoreCase) && !t.SystemName.IsNullOrWhiteSpace() && t.SystemName.Equals(bussionModule, StringComparison.InvariantCultureIgnoreCase));
            ////兼容页面在A系统，但菜单挂在B系统的问题
            //if (!flag)
            //{
            //    flag = powerList.Any(t => !t.ActionUrl.IsNullOrWhiteSpace() && t.ActionUrl.EndsWith(actionUrl, StringComparison.InvariantCultureIgnoreCase));
            //}
            return true;

        }
    }
}
