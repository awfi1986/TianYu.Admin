using TianYu.Core.Common;
using TianYu.Admin.Service.IService; 
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Api;
using TianYu.Admin.Domain.ViewModel.Request; 

namespace TianYu.Admin.WebMvc.Controllers
{
    [ValidateLogin]
    public class SystemRoleController : BaseController
    {
        public ISystemRoleService _systemRoleService { get; set; } 
        public ISystemRoleRulesService _systemRoleRulesService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Edit(QueryDetailSystemRoleRequestModel requestModel)
        {
            var systemRole = _systemRoleService.QueryDetail(requestModel);
            return View(systemRole.BusinessData);

        }
        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public ActionResult PowerSettings(int roleId)
        {
            ViewBag.RoleId = roleId;

            //获取所有菜单以及角色权限
            var menuList = _systemRoleService.FindMenuButtonRole(roleId); 

            return View(menuList);
        } 

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Insert(AddSystemRoleRequestModel requestModel)
        {
            var res = _systemRoleService.Insert(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 角色查询
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Query(QuerySystemRoleRequestModel requestModel)
        {
            var res = _systemRoleService.Query(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Update(UpdateSystemRoleRequestModel requestModel)
        {
            var res = _systemRoleService.Update(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult Remove(RemoveSystemRoleRequestModel requestModel)
        {
            var res = _systemRoleService.Remove(requestModel);
            return Content(res.ToJsonString());
        }
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ActionResult SaveRolePowers(SetSystemRoleRulesRequestModel requestModel)
        {
            var ret = _systemRoleRulesService.SetSystemRoleRules(requestModel);
            return Content(ret.ToJsonString());
        } 
    }
}