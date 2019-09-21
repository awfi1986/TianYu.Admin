﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using TianYu.Admin.Domain;
using TianYu.Admin.Domain.ViewModel.Request;
using TianYu.Admin.Domain.ViewModel.Response;
using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Admin.Service.Service
{
    /// <summary> 
	/// 数据字典业务逻辑类
	/// </summary> 
    public class SystemDictionaryService :ISystemDictionaryService
    {
        private readonly ISystemDictionaryRepository _systemDictionaryRepository;
        public SystemDictionaryService(ISystemDictionaryRepository systemDictionaryRepository)
        {
            this._systemDictionaryRepository = systemDictionaryRepository;
        }

		/// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<string> Insert(AddSystemDictionaryRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<string>() { Status=ResponseStatus.Fail};


            res.Status = ResponseStatus.Success; 
            return res;
        } 
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<string> Remove(RemoveSystemDictionaryRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail };


            res.Status = ResponseStatus.Success;
            return res;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<string> Update(UpdateSystemDictionaryRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail };


            res.Status = ResponseStatus.Success;
            return res;
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessPagedBaseViewModel<QuerySystemDictionaryResponseModel> Query(QuerySystemDictionaryRequestModel requestModel)
        {
            var res = new BusinessPagedBaseViewModel<QuerySystemDictionaryResponseModel>() { Status = ResponseStatus.Fail };


            res.Status = ResponseStatus.Success;
            return res;
        }
        /// <summary>
        /// 查询明细
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<QueryDetailSystemDictionaryResponseModel> QueryDetail(QueryDetailSystemDictionaryRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<QueryDetailSystemDictionaryResponseModel>() { Status = ResponseStatus.Fail };


            res.Status = ResponseStatus.Success;
            return res;
        }
    }
}



