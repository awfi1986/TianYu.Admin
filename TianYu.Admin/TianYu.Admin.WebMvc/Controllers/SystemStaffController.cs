using TianYu.Core.Common;
using TianYu.Admin.Service.IService;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;
using TianYu.Admin.Domain.ViewModel.Request;
using System.Collections.Generic;
using System.Linq;

namespace TianYu.Admin.WebMvc.Controllers
{
    /// <summary>
    /// 成员控制器
    /// </summary>
    [MvcLoginFilter]
    public class SystemStaffController : BaseController
    {
        private ISystemStaffService _systemStaffService = null;
        private ISystemRoleService _systemRoleService = null;
        private ISystemSectionService _systemSectionService = null;
        private ISystemStaffRoleService _systemStaffRoleService;
        public SystemStaffController(ISystemStaffService systemStaffService, ISystemRoleService systemRoleService, ISystemSectionService systemSectionService
            , ISystemStaffRoleService systemStaffRoleService)
        {
            this._systemStaffService = systemStaffService;
            this._systemRoleService = systemRoleService;
            this._systemSectionService = systemSectionService;
            this._systemStaffRoleService = systemStaffRoleService;
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
            var roleList = _systemRoleService.GetSystemRole();
            IEnumerable<SelectListItem> Roles = roleList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoleName,
                Selected = false,
            });
            ViewBag.Roles = Roles;

            var sectionList = _systemSectionService.GetSystemSection();
            IEnumerable<SelectListItem> Sections = sectionList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = false,
            });
            ViewBag.Sections = Sections;
            return View();
        }
        /// <summary>
        /// 编辑
        /// </summary> 
        /// <param name="requestModel"></param>
        /// <returns></returns> 
        public ActionResult Edit(QueryDetailSystemStaffRequestModel requestModel)
        {
            var staffRoleList = _systemStaffRoleService.QueryStaffRoleByStaffId(requestModel.Id);
            var roleList = _systemRoleService.GetSystemRole();
            IEnumerable<SelectListItem> Roles = roleList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoleName,
                Selected = staffRoleList.Where(y => y.RoleId == x.Id).Count() > 0,
            });
            ViewBag.Roles = Roles;

            var sectionList = _systemSectionService.GetSystemSection();
            IEnumerable<SelectListItem> Sections = sectionList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = false,
            });
            ViewBag.Sections = Sections;
             
            var res = _systemStaffService.QueryDetail(requestModel);
            return View(res.BusinessData);
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns> 
        public ActionResult Again(ResetSystemStaffRequestModel requestModel)
        {
            //ViewBag.Id = request.Id;

            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdPwd()
        {
            return View();
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Query(QuerySystemStaffRequestModel requestModel)
        {
            var res = _systemStaffService.Query(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Insert(AddSystemStaffRequestModel requestModel)
        {
            var ret = _systemStaffService.Insert(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Update(UpdateSystemStaffRequestModel requestModel)
        {
            var ret = _systemStaffService.Update(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Remove(RemoveSystemStaffRequestModel requestModel)
        {
            var ret = _systemStaffService.Remove(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Reset(ResetSystemStaffRequestModel requestModel)
        {
            var ret = _systemStaffService.Reset(requestModel);
            return Content(ret.ToJsonString());
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult UpdatePwd(UpdatePwdSystemStaffRequestModel requestModel)
        {
            var ret = _systemStaffService.UpdatePwd(requestModel,base.GetLoginUserInfo.Id);
            return Content(ret.ToJsonString());
        }
    }
}