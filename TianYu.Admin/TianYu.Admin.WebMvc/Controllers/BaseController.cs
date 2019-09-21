using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Infrastructure.Expression;
using TianYu.Admin.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;
using TianYu.Admin.Domain.ViewModel;
using TianYu.Core.Cache;

namespace TianYu.Admin.WebMvc
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <returns></returns>
        public SystemLoginUserInfo GetLoginUserInfo
        {
            get
            {
                SystemLoginUserInfo userInfo = null;
                var token = CookieHelper.GetCookieValue(TianYuConsts.SystemLoginCookieName);
                if (!token.IsNullOrWhiteSpace())
                {
                    userInfo = CacheHelper.Get<SystemLoginUserInfo>(TianYuConsts.GetLoginUserInfoCacheKey(token));
                }
                return userInfo;
            }
        }
        /// <summary>
        /// 获取登录用户菜单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemMenuRoleViewModel> GetLoginUserMenu(string controllerName = "")
        {
            IEnumerable<SystemMenuRoleViewModel> menuList = null;
            var token = CookieHelper.GetCookieValue(TianYuConsts.SystemLoginCookieName);
            if (!token.IsNullOrWhiteSpace())
            {
                menuList = CacheHelper.Get<IEnumerable<SystemMenuRoleViewModel>>(TianYuConsts.GetLoginUserMenuCacheKey(token));

                if (menuList != null && !controllerName.IsNullOrWhiteSpace())
                {
                    menuList = menuList.Where(x => x.MenuUrl != null && x.MenuUrl.Contains(controllerName)).ToList();
                }
            }
            return menuList;
        }
        /// <summary>
        /// 获取登录用户按钮操作权限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemButtonRoleViewModel> GetLoginAccountButtonRole(int menuId)
        {
            IEnumerable<SystemButtonRoleViewModel> buttonList = null;
            var token = CookieHelper.GetCookieValue(TianYuConsts.SystemLoginCookieName);
            if (!token.IsNullOrWhiteSpace())
            {
                buttonList = CacheHelper.Get<IEnumerable<SystemButtonRoleViewModel>>(TianYuConsts.GetLoginUserButtonCacheKey(token));

                if (menuId != 0)
                {
                    buttonList = buttonList.Where(x => x.MenuId == menuId).ToList();
                }
            }
            return buttonList;
        }

        #region 下拉框
        /// <summary>
        /// 根据枚举获取选项列表
        /// </summary>
        /// <typeparam name="TEnums">枚举类型</typeparam>
        /// <param name="isFirst">是否添加第一行</param>
        /// <param name="firstText">第一行文本</param>
        /// <param name="isDescription">是否使用描述</param>
        /// <param name="selectedValue">默认选中值</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectListItemByEnum<TEnums>(bool isFirst = true, string firstText = "", bool isDescription = true, string selectedValue = "") where TEnums : new()
        {
            var items = EnumExpression.GetEnumList<TEnums>().Select(a => new SelectListItem
            {
                Value = a.ToString("D"),
                Text = isDescription ? a.GetEnumDescription() : a.ToString(),
                Selected = a.ToString("D") == selectedValue,
            }).ToList();

            if (isFirst)
            {
                items.Insert(0, GetFirstSelectListItem("", firstText));
            }
            return items;
        }
        /// <summary>
        /// 获取第一行选项
        /// </summary>
        /// <param name="firstValue">第一行值</param>
        /// <param name="firstText">第一行文本</param>
        /// <returns></returns>
        public SelectListItem GetFirstSelectListItem(string firstValue = "", string firstText = "")
        {
            return new SelectListItem
            {
                Text = firstText,
                Value = firstValue,
            };
        }
        #endregion 

        #region 错误页面处理
        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public ActionResult GoErrorPage(string msg)
        {
            msg = this.HttpContext.Server.UrlEncode(msg);
            return RedirectToAction("Index", "Error", new { area = "", msg });
        }

        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="response">响应对象</param>
        /// <returns></returns>
        public ActionResult GoErrorPage<T>(BusinessBaseViewModel<T> response)
        {
            if (this.Request.IsAjaxRequest())
            {
                return Content(response.ToJsonString());
            }
            else
            {
                //if (response.Status == ResponseStatus.UnLoginIn || response.Status == ResponseStatus.SessionIdError)
                //{
                //    string returnUrl = Convert.ToBase64String(Encoding.UTF8.GetBytes(HttpContext.Request.Url.ToString()));
                //    return RedirectToAction("Login", "Home", new { area = "", returnUrl });
                //}
                //else
                //{
                return GoErrorPage(response.ErrorMessage);
                //}
            }
        }

        #endregion

        /// <summary>
        /// 重写动作执行之前方法，获取当前页面的操作按钮
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //处理视图使用文件资源的url
            ViewBag.FileResourcesDomain = ConfigHelper.GetAppsettingValue(TianYuConsts.FileServerUrl).TrimEnd('/');
            //页面操作权限
            ViewBag.UserActions = new List<SystemButtonRoleViewModel>();

            if (!(filterContext.ActionDescriptor.GetCustomAttributes(typeof(MvcIgnorePowerAttribute), true).Any()
                || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(MvcIgnorePowerAttribute), true).Any()))
            {
                //权限校验的，需要设置操作按钮
                //string areaName = filterContext.RouteData.DataTokens["area"].IsNull() ? "" : filterContext.RouteData.DataTokens["area"].ToString();
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
                string actionName = filterContext.RouteData.Values["action"].ToString();

                var menuList = GetLoginUserMenu(controllerName);

                if (menuList != null)
                {
                    var menuEntity = menuList.FirstOrDefault();
                    if (menuEntity != null)
                    {
                        ViewBag.UserActions = GetLoginAccountButtonRole(menuEntity.Id);

                        if (!menuEntity.MenuUrl.IsNullOrWhiteSpace())
                        { 
                            if (!menuEntity.PageTitle.IsNullOrWhiteSpace())
                            {
                                var pageTitle = menuEntity.PageTitle.Split('-');

                                for (int i = 0; i < pageTitle.Length; i++)
                                {
                                    if (i != pageTitle.Length - 1)
                                    {
                                        ViewBag.PageTitle += $"<a>{pageTitle[i]}</a>";
                                    }
                                    else
                                    {
                                        ViewBag.PageTitle += $"<a><cite>{pageTitle[i]}</cite></a>";
                                    } 
                                }
                            }
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}