using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Log.FilterAttribute
{
    /// <summary>
    /// 忽略日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreLogAttribute : Attribute
    {
    }
}
