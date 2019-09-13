using TianYu.Core.Common.BaseEF;
using TianYu.Admin.Domain;
using TianYu.Admin.Infrastructure.Constant;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace TianYu.Admin.Repository.DbFactory
{
    public class DbContextFactory
    {
        /// <summary>
        /// 创建EF上下文对象,已存在就直接取,不存在就创建,保证线程内是唯一。
        /// </summary>
        public static DbContext PowerDbCreate
        {
            get
            {
                DbContext dbContext = CallContext.GetData("SystemPowerDbContext") as DbContext;
                if (dbContext == null)
                {
                    dbContext = new TianYuSystemPowerContext(ConfigConstants.SystemPowerConnectionString);
                    CallContext.SetData("SystemPowerDbContext", dbContext);
                }
                return dbContext;
            }
        }
        /// <summary>
        /// 创建工作单元对象,已存在就直接取,不存在就创建,保证线程内是唯一。
        /// </summary>
        public static EFUnitOfWork PowerDbUnitOfWorkCreate
        {
            get
            {
                EFUnitOfWork unitOfWork = CallContext.GetData("SystemPowerDbUnitOfWorkContext") as EFUnitOfWork;
                if (unitOfWork == null)
                {
                    unitOfWork = new EFUnitOfWork(PowerDbCreate);
                    CallContext.SetData("SystemPowerDbUnitOfWorkContext", unitOfWork);
                }
                return unitOfWork;
            }
        }
    }
}
