using System.Threading.Tasks;

namespace TianYu.Core.Common.UnitOfWork
{
    /// <summary>
    /// 工作单元基类接口
    /// </summary>
   public  interface IUnitOfWork
    {
        bool isCommitted { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
