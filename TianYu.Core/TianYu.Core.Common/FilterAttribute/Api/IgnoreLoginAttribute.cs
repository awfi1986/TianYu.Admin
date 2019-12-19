using System; 

namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 忽略登录验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreLoginAttribute : Attribute
    {
    }
}
