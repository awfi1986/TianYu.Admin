using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.Security
{
    /// <summary>
    /// 忽略票据检验
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreAuthenticationAttribute:Attribute
    {
    }
}
