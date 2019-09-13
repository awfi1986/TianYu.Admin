using TianYu.Core.Common.BaseEF;
using TianYu.Core.Common.UnitOfWork;
using TianYu.Admin.Repository.DbFactory;

namespace TianYu.Admin.Repository.BaseRepository
{
    public class EFRepository<TEntity> : EFBaseRepository<TEntity> where TEntity : BaseAggregateRoot
    {
        public EFRepository() : base(DbContextFactory.PowerDbUnitOfWorkCreate)
        {
        }
    }
}
