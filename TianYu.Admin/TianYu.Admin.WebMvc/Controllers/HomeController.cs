using TianYu.Core.Cache;
using TianYu.Core.Common;
using TianYu.Admin.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;
using TianYu.Admin.Domain.ViewModel;
using TianYu.Core.Common.BaseViewModel;
using System.Text;

namespace TianYu.Admin.WebMvc.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary> 
    public class HomeController : BaseController
    {
        private ISystemStaffService SystemStaffService { get; set; }
        public HomeController(ISystemStaffService StaffService)
        {
            SystemStaffService = StaffService;
        }

        /// <summary>
        /// 后台欢迎页
        /// </summary>
        /// <returns></returns> 
        [MvcLoginFilter]
        public ActionResult Wecome()
        {
            return View();
        }
        /// <summary>
        /// 后台首页
        /// </summary>
        /// <param name="logInDefault"></param>
        /// <returns></returns> 
        [MvcLoginFilter]
        public ActionResult Index(int logInDefault = 0)
        {
            //获取该用户拥有的相关模块 
            var cookie = Request.Cookies.Get(TianYuConsts.SystemLoginCookieName);

            var menuViewModel = CacheHelper.Get<IEnumerable<SystemMenuRoleViewModel>>(TianYuConsts.GetLoginUserMenuCacheKey(cookie.Value));
            //读取一级菜单
            var oneLevelMenu = menuViewModel.Where(x => x.Level == 1).OrderBy(x => x.MenuSort).ToList();

            ViewBag.OneLevelMenu = oneLevelMenu;

            if (logInDefault == 0 && oneLevelMenu != null)
            {
                logInDefault = oneLevelMenu.FirstOrDefault().Id;
            }
            ViewBag.Refres = logInDefault;

            //读取一级以下菜单
            ViewBag.OtherLevelMenuHtml = GetSubMenuHtml(menuViewModel.ToList(), logInDefault);

            //ViewBag.LogoutUrl = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemManagerLogoutDomain + "/Home/Login");

            var loginInfo = CacheHelper.Get<SystemLoginUserInfo>(TianYuConsts.GetLoginUserInfoCacheKey(cookie.Value));

            return View(loginInfo);
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [MvcIgnoreLogin, MvcIgnorePower]
        public ActionResult Login(string redirectUrl = "")
        {
            string text = Request.Url.OriginalString;

            if (redirectUrl.IsNullOrWhiteSpace())
            {
                redirectUrl = "/Home/Index";
            }
            ViewBag.RedirectUrl = HttpUtility.UrlDecode(redirectUrl, System.Text.Encoding.UTF8);
            return View();
        }


        /// <summary>
        /// 登录后台
        /// </summary>
        /// <param name="p"></param>
        /// <param name="u"></param>
        /// <param name="redUrl"></param>
        /// <returns></returns> 
        [MvcIgnoreLogin, MvcIgnorePower]
        public string Api_Login(string u, string p, string redUrl)
        {
            var result = SystemStaffService.Login(u, p);
            if (result.Status == ResponseStatus.Success)
            {
                HttpCookie cookie = null;
                if (Request.Cookies.Count > 0)
                {
                    cookie = Request.Cookies.Get(TianYuConsts.SystemLoginCookieName);
                }
                if (cookie == null)
                {
                    cookie = new HttpCookie(TianYuConsts.SystemLoginCookieName);
                }

                cookie.Value = result.BusinessData;
                cookie.Domain = ConfigHelper.GetAppsettingValue("CookieDomainName");
                cookie.Expires = DateTime.Now.AddHours(24);
                cookie.HttpOnly = false;
                cookie.Path = "/";
                Response.Cookies.Add(cookie);

                if (redUrl.IsNullOrWhiteSpace())
                {
                    redUrl = "/Home/Index";
                }
                result.BusinessData = redUrl;
            }
            return result.ToJsonString();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns> 
        public ActionResult Api_Logout()
        {
            SystemStaffService.Logout();
            return RedirectToAction("Login", "Home");
        }


        /// <summary>
        /// 递归获取当前登录用户一级以下菜单HTML 
        /// </summary>
        /// <returns></returns>
        private string GetSubMenuHtml(List<SystemMenuRoleViewModel> menuList, int parentId)
        {
            //组装HTML标签
            var html = new StringBuilder();

            var list = menuList.Where(x => x.ParentId == parentId).OrderBy(x => x.MenuSort).ToList();

            foreach (var node in list)
            {
                html.AppendLine("<li>");
                html.AppendLine(node.MenuUrl.IsNullOrWhiteSpace() ? "<a href=\"javascript:;\">" : $"<a _href=\"{node.MenuUrl}\">");
                html.AppendLine($"<i class=\"iconfont\">{(node.MenuIcon.IsNullOrWhiteSpace() ? "&#xe6b8;" : node.MenuIcon)}</i>");
                html.AppendLine($"<cite>{node.MenuName}</cite>");
                html.AppendLine("<i class=\"iconfont nav_right\">&#xe697;</i>");
                html.AppendLine("</a>");
                if (node.MenuUrl.IsNullOrWhiteSpace())
                {
                    html.AppendLine("<ul class=\"sub-menu\">");
                    html.AppendLine(GetSubMenuHtml(menuList, node.Id));
                    html.AppendLine("</ul>");
                }
                html.AppendLine("</li>");
            }

            return html.ToString();
        }


    }
}