using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.UnitOfWork
{
    /// <summary>
    /// 表示EF的工作单元接口，因为DbContext是EF的对象
    /// </summary>
    public interface IEFUnitOfWork:IUnitOfWorkRepositoryContext
    {
        DbContext context { get; }
    }
}
