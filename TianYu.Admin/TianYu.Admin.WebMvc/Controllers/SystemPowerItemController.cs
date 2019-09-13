﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using TianYu.Core.Common;
using TianYu.Admin.Service.IService; 
using System.Web.Mvc; 
using TianYu.Core.Common.FilterAttribute.Mvc;
using TianYu.Admin.Domain.ViewModel.Request;

namespace TianYu.Admin.WebMvc.Controllers
{
    /// <summary>
    /// 权限项控制器
    /// </summary>
    [MvcLoginFilter]
    public class SystemPowerItemController : BaseController
    {
        private ISystemPowerItemService _systemPowerItemService = null; 
        public SystemPowerItemController(ISystemPowerItemService systemPowerItemService)
        {
            this._systemPowerItemService = systemPowerItemService; 
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns> 
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>  
        public ActionResult Add()
        {  
            return View();
        }
        /// <summary>
        /// 编辑
        /// </summary> 
		/// <param name="requestModel"></param>
        /// <returns></returns> 
        public ActionResult Edit(QueryDetailSystemPowerItemRequestModel requestModel)
        { 
            var res = _systemPowerItemService.QueryDetail(requestModel);
            return View(res.ToJsonString());
        } 


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Query(QuerySystemPowerItemRequestModel requestModel)
        {
            var res = _systemPowerItemService.Query(requestModel);
            return Content(res.ToJsonString());
        } 
        /// <summary>
        /// 添加
        /// </summary>
		/// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Insert(AddSystemPowerItemRequestModel requestModel)
        {
            var ret = _systemPowerItemService.Insert(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Update(UpdateSystemPowerItemRequestModel requestModel)
        {
            var ret = _systemPowerItemService.Update(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Remove(RemoveSystemPowerItemRequestModel requestModel)
        {
            var ret = _systemPowerItemService.Remove(requestModel);
            return Content(ret.ToJsonString());
        } 
    }
}
