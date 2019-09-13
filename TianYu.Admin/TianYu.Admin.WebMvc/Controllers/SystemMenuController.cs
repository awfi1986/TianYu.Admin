using TianYu.Core.Common;
using TianYu.Admin.Domain.ViewModel;
using TianYu.Admin.Infrastructure.Enum;
using TianYu.Admin.Service.IService;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Api;
using TianYu.Admin.Domain.ViewModel.Request;
using System;

namespace TianYu.Admin.WebMvc.Controllers
{
    [ValidateLogin]
    public class SystemMenuController : BaseController
    {
        public ISystemMenuService _systemMenuService { get; set; }
        public ISystemActionButtonService _systemActionButtonService { get; set; }


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
        public ActionResult Add(int pId,string pCode, int pLevel, string pName)
        {
            ViewBag.pCode = pCode;
            ViewBag.pLevel = pLevel + 1;
            ViewBag.pName = pName;
            ViewBag.pId = pId; 

            var buttonList = _systemActionButtonService.GetSystemButtons();

            return View(buttonList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(QueryDetailSystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.QueryDetail(requestModel);
            return View(res.BusinessData);
        }


        public ActionResult Query(QuerySystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.Query(requestModel);

            return Content(res.ToJsonString());
        }

        public ActionResult Update(UpdateSystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.Update(requestModel);

            return Content(res.ToJsonString());
        }
        public ActionResult Remove(RemoveSystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.Remove(requestModel);

            return Content(res.ToJsonString());
        }
        public ActionResult Insert(AddSystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.Insert(requestModel);

            return Content(res.ToJsonString());
        }
        public ActionResult Enabled(EnabledSystemMenuRequestModel requestModel)
        {
            var res = _systemMenuService.Enabled(requestModel);

            return Content(res.ToJsonString());
        }
    }
}