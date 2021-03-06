﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using TianYu.Core.Common.BaseViewModel;
using System.Collections.Generic;
using TianYu.Admin.Domain.ViewModel.Response;
using TianYu.Admin.Domain.ViewModel.Request; 

namespace TianYu.Admin.Service.IService
{
    /// <summary>
    /// 角色权限服务接口
    /// </summary>
    public interface ISystemRoleRulesService
    {
        /// <summary>
        ///查询角色权限
        /// </summary>
        /// <returns></returns>
        BusinessBaseViewModel<IList<QuerySystemRoleRulesResponseModel>> Query(QuerySystemRoleRulesRequestModel requestModel);
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="requestModels"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> SetSystemRoleRules(SetSystemRoleRulesRequestModel requestModels);
    }
}


