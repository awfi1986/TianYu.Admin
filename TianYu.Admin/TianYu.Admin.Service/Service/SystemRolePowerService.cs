using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain;
using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using System.Linq;
using System.Transactions;
using TianYu.Core.Common;
using TianYu.Admin.Domain.ViewModel.Request;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TianYu.Admin.Service.Service
{
    public class SystemRolePowerService : ISystemRolePowerService
    {
        private readonly ISystemRolePowerRepository _systemRolePowerRepository;
        private readonly ISystemPowerItemRepository _systemPowerItemRepository;
        public SystemRolePowerService(ISystemRolePowerRepository systemRolePowerRepository, ISystemPowerItemRepository systemPowerItemRepository)
        {
            this._systemRolePowerRepository = systemRolePowerRepository;
            this._systemPowerItemRepository = systemPowerItemRepository;
        }
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<string> SetRolePower(SetSystemRolePowerRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail };

            if (requestModel == null)
            {
                res.ErrorMessage = "参数错误";
                return res;
            }
            if (requestModel.RoleId <= 0)
            {
                res.ErrorMessage = "角色Id不能为空";
                return res;
            }
            var rolePowerList = new List<SystemRolePower>();

            if (requestModel.PowerIdList != null)
            {
                foreach (var m in requestModel.PowerIdList)
                {
                    rolePowerList.Add(new SystemRolePower()
                    {
                        PowerId = m,
                        RoleId = requestModel.RoleId
                    });
                }
            }

            _systemRolePowerRepository.SqlExecute("delete s_SystemRolePower where RoleId = @RoleId", new[] { new SqlParameter("RoleId", requestModel.RoleId) });
            if (rolePowerList != null)
            {
                _systemRolePowerRepository.Insert(rolePowerList);
            }
            _systemRolePowerRepository.SaveChanges();

            res.Status = ResponseStatus.Success;
            return res;
        }
    }
}
