using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 系统公用常量
    /// </summary>
    public class TianYuConsts
    {
        /// <summary>
        /// 系统后台cookie
        /// </summary>
        public const string SystemLoginCookieName = "_aa_default_login_code_aa";
        /// <summary>
        /// 系统后台模块
        /// </summary>
        public const string SystemManagerModuleName = "SystemManager_Module_Name";
        /// <summary>
        /// 系统后台登录登出域名
        /// </summary>
        public const string SystemManagerLogoutDomain = "SystemManagerLogoutDomain"; 
        /// <summary>
        /// 系统权限接口地址
        /// </summary>
        public const string SystemManagerApiUrl = "SystemManagerApiUrl";
        /// <summary>
        /// 系统接口API白名单
        /// </summary>
        public const string ApiIpWhiteList = "ApiIpWhiteList";

        /// <summary>
        /// 系统接口API授权DB链接串
        /// </summary>
        public const string ApiAuthorizeDatabase = "ApiAuthorizeDatabase";
        /// <summary>
        /// 根域名
        /// </summary>
        public const string DefaultDomainName = "TianYunet.com";
        /// <summary>
        /// 文件上传保存根目录配置项
        /// </summary>
        public const string SystemFileUploadRootPath = "SystemFileUploadRootPath";
        /// <summary>
        /// file 服务器文件根目录
        /// </summary>
        public const string SystemFileUploadRootUrl = "SystemFileUploadRootUrl";
        /// <summary>
        /// 允许上传文件的扩展名
        /// </summary>
        public const string SystemFileUploadExtName = "SystemFileUploadExtName";
        /// <summary>
        /// 文件服务器地址
        /// </summary>
        public const string FileServerUrl = "FileServerUrl";
        /// <summary>
        /// 小数位数
        /// </summary>
        public const int Decimals = 6;

        #region API 登录会话配置
        /// <summary>
        /// api当前用户组对象键名
        /// </summary>
        public const string WebApiCurrentUserGroup = "CurrentUserGroup";
        /// <summary>
        /// api登录会话Id键名
        /// </summary>
        public const string WebApiSessionId = "SessionId";
        /// <summary>
        /// API版本号键名
        /// </summary>
        public const string ApiVersionKey = "ApiVersion";
        /// <summary>
        /// 登录会话缓存键，用于存取登录用户信息
        /// </summary>
        /// <param name="sessionid">登录会话Id</param>
        public static string GetSessionIdCacheKey(string sessionId)
        {
            return $"TianYu:UserLogin:SessionID:{sessionId}";
        }
        /// <summary>
        /// 登录用户菜单缓存key
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static string GetLoginUserMenuCacheKey(string sessionId)
        {
            return $"TianYu:UserLogin:Menu:{sessionId}";
        }
        /// <summary>
        /// 登录用户按钮缓存key
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static string GetLoginUserButtonCacheKey(string sessionId)
        {
            return $"TianYu:UserLogin:Button:{sessionId}";
        }
        /// <summary>
        /// 登录用户信息缓存key
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static string GetLoginUserInfoCacheKey(string sessionId)
        {
            return $"TianYu:UserLogin:Info:{sessionId}";
        }
        #endregion

        #region 系统管理后台登录会话配置

        #endregion
        #region 接口ticket 配置
        /// <summary>
        /// 接口ticket 配置 缓存键，用于存取接口ticket信息
        /// </summary>
        /// <param name="token">登录会话Id</param>
        public static string GetTicketCacheKey(string token)
        {
            return $"TianYu:ApiToken:ticket:{token}";
        }
        #endregion

        /// <summary>
        /// 默认logo图片路径配置项
        /// </summary>
        public const string LogoImagePath = "LogoImagePath";
        /// <summary>
        /// 红包分享背景图
        /// </summary>
        public const string RedShareBackImg = "RedShareBackImg";
    }
}
