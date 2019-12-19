using System;
namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 不验证请求鉴权
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreAuthorizeAttribute : Attribute
    {
    }
}