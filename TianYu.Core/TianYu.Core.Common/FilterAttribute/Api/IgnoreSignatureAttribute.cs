using System;
namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 不验证请求参数签名
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreSignatureAttribute : Attribute
    {
    }
}