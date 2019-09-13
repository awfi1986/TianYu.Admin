﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System.Collections.Generic;
using System.Linq;
using TianYu.Admin.Domain.ViewModel.Response;
using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using TianYu.Core.Common;

namespace TianYu.Admin.Service.Service
{
    /// <summary> 
	/// 按键仓储服务类
	/// </summary> 
    public class SystemActionButtonService : ISystemActionButtonService
    {
        private readonly ISystemActionButtonRepository _systemActionButtonRepository;
        public SystemActionButtonService(ISystemActionButtonRepository systemActionButtonRepository)
        {
            this._systemActionButtonRepository = systemActionButtonRepository;
        }
        /// <summary>
        /// 获取系统button
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemButton> GetSystemButtons()
        {
            return _systemActionButtonRepository.Find(x => x.Enabled == true).MapToList<SystemButton>();
        }
    }
}



