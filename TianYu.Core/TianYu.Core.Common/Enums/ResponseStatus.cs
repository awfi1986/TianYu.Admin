using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    public enum ResponseStatus
    {
        #region 系统错误码
        /// <summary>
        /// 未赋值状态码
        /// </summary>
        [Description("未赋值状态码")]
        None = -1,
        /// <summary>
        /// 业务处理失败
        /// </summary>
        [Description("业务处理失败")]
        Fail = 0,

        /// <summary>
        /// 业务处理成功
        /// </summary>
        [Description("业务处理成功")]
        Success = 1,
        /// <summary>
        /// 请求参数错误
        /// </summary>
        [Description("请求参数错误")]
        ParameterError = 9,
        /// <summary>
        /// 请求参数大于最大长度限制
        /// </summary>
        [Description("请求参数大于最大长度限制")]
        ParameterLengthError = 10,
        /// <summary>
        /// 请求票据错误
        /// </summary>
        [Description("请求票据错误")]
        AuthenticationTicketError = 21,
        /// <summary>
        /// 请求票据超时
        /// </summary>
        [Description("请求票据超时")]
        AuthenticationTicketTimeOut = 22,
        /// <summary>
        /// 缺少Signature请求参数
        /// </summary>
        [Description("缺少Signature请求参数")]
        UnSignatureParamsError = 31,
        /// <summary>
        /// 请求时间戳格式错误
        /// </summary>
        [Description("请求时间戳格式错误")]
        UnTimeSpanFromatError = 32,
        /// <summary>
        /// 请求签名时间戳超时
        /// </summary>
        [Description("请求签名时间戳超时")]
        UnTimeSpanError = 33,
        /// <summary>
        /// 缺少TimeSpan请求参数
        /// </summary>
        [Description("缺少TimeSpan请求参数")]
        UnTimeSpanParamsError = 34,
        /// <summary>
        /// 缺少登录会话Id参数
        /// </summary>
        [Description("缺少登录会话Id参数")]
        UnSessionIdParamsError = 35,
        /// <summary>
        /// 登录会话Id无效
        /// </summary>
        [Description("登录会话Id无效")]
        SessionIdError = 36,
        /// <summary>
        /// 登录会话Id在其他设备登录
        /// </summary>
        [Description("您的账号已在其他设备登录")]
        SessionIdOtherLogin = 37,
        /// <summary>
        /// 服务端拒绝访问：你没有权限进行操作
        /// </summary>
        [Description("服务端拒绝访问：你没有权限进行操作")]
        UnAuthorityRightsError = 402,
        /// <summary>
        /// 服务端拒绝访问：你没有权限访问
        /// </summary>
        [Description("服务端拒绝访问：你没有权限访问")]
        UnAuthorityError = 403,
        /// <summary>
        /// 请求签名不正确
        /// </summary>
        [Description("请求签名不正确")]
        UnSignatureError = 401,
        /// <summary>
        /// 业务处理异常
        /// </summary>
        [Description("业务处理异常")]
        BusinessError = 99,
        /// <summary>
        /// 接口异常
        /// </summary>
        [Description("接口异常")]
        SystemError = 999,

        #endregion

        #region 登录、注册等错误码，预留值范围101-120

        /// <summary>
        /// 登录密码错误
        /// </summary>
        [Description("登录密码错误")]
        LoginPwdError = 101,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        UserIsNotExists = 102,

        /// <summary>
        /// 需要重置密码
        /// </summary>
        [Description("需要重置密码")]
        NeedResetPwd=103,

        /// <summary>
        /// 需要设置密码
        /// </summary>
        [Description("需要设置密码")]
        NeedSettingPwd = 104,

        #endregion

    }
}
