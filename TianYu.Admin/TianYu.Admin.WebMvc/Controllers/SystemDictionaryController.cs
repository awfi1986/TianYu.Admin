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
    /// 数据字典控制器
    /// </summary>
    [MvcLoginFilter]
    public class SystemDictionaryController : BaseController
    {
        private ISystemDictionaryService _systemDictionaryService = null;
        public SystemDictionaryController(ISystemDictionaryService systemDictionaryService)
        {
            this._systemDictionaryService = systemDictionaryService;
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
        public ActionResult Add(int parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }
        /// <summary>
        /// 编辑
        /// </summary> 
		/// <param name="requestModel"></param>
        /// <returns></returns> 
        public ActionResult Edit(QueryDetailSystemDictionaryRequestModel requestModel)
        {
            var res = _systemDictionaryService.QueryDetail(requestModel);
            return View(res.BusinessData);
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Query(QuerySystemDictionaryRequestModel requestModel)
        {
            var res = _systemDictionaryService.Query(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Insert(AddSystemDictionaryRequestModel requestModel)
        {
            var ret = _systemDictionaryService.Insert(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Update(UpdateSystemDictionaryRequestModel requestModel)
        {
            var ret = _systemDictionaryService.Update(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Remove(RemoveSystemDictionaryRequestModel requestModel)
        {
            var ret = _systemDictionaryService.Remove(requestModel);
            return Content(ret.ToJsonString());
        }
    }
}
