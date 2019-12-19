using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.Mvc
{
    /// <summary>
    /// 忽略登录验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MvcIgnoreLoginAttribute: Attribute
    {
    }
}
