﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain.ViewModel.Response;
using TianYu.Admin.Domain.ViewModel.Request;

namespace TianYu.Admin.Service.IService
{
    /// <summary>
    /// 系统账号服务接口
    /// </summary>
    public interface ISystemStaffService
    {
        /// <summary>
        /// 登录系统后台
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Login(string loginName, string password);
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        BusinessBaseViewModel<string> Logout();
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessPagedBaseViewModel<QuerySystemStaffResponseModel> Query(QuerySystemStaffRequestModel requestModel);
        /// <summary>
        /// 获取用户明细
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessBaseViewModel<QueryDetailSystemStaffResponseModel> QueryDetail(QueryDetailSystemStaffRequestModel requestModel);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Insert(AddSystemStaffRequestModel requestModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Update(UpdateSystemStaffRequestModel requestModel);
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Remove(RemoveSystemStaffRequestModel requestModel);
        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Reset(ResetSystemStaffRequestModel requestModel);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> UpdatePwd(UpdatePwdSystemStaffRequestModel requestModel, int id);
    }
}


