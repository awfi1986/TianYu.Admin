using Newtonsoft.Json;
using TianYu.Core.Common; 
using TianYu.Admin.Domain.ViewModel;
using TianYu.Admin.Infrastructure.Enum;
using TianYu.Admin.Service.IService; 
using System.Collections.Generic; 
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;
using TianYu.Core.Common.FilterAttribute.Api;

namespace TianYu.Admin.WebMvc.Controllers
{
    [ValidateLogin]
    public class MQBusinessController : BaseController
    {
        #region 变量
        private IMQBusinessConfigService _mQBusinessConfigService;
        #endregion

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mQBusinessConfigService"></param>
        public MQBusinessController(IMQBusinessConfigService mQBusinessConfigService)
        {
            this._mQBusinessConfigService = mQBusinessConfigService;
        }

        #endregion

        #region 视图

        [MvcPowerFilter]
        public ActionResult Index()
        {
            GetListItemByEnum();
            return View();
        }
        [MvcPowerFilter]
        public ActionResult Adds()
        {
            var model = new MQBusinessResponse();
            GetListItemByEnum();
            string mqRequestUrl= ConfigHelper.GetAppsettingValue("MqRequestUrl");
            var mqRequestList=JsonConvert.DeserializeObject<List<MqRequestUrlResponse>>(mqRequestUrl);
            List<SelectListItem> selectList = new List<SelectListItem>() { new SelectListItem() { Text = "请选择", Value = "" } };
            mqRequestList.ForEach(a => {
                selectList.Add(new SelectListItem() { Text=a.InterfaceName,Value=a.Url});
            });
            ViewBag.MqRequestUrl = selectList;



            return View(model);
        }
        [MvcPowerFilter]
        public ActionResult Edits(MQBusinessInfoRequest request)
        {
            GetListItemByEnum();
            var model = _mQBusinessConfigService.FindInfoById(request.ID);
            if (model == null)
            {
                return GoErrorPage("任务信息不存在或已被删除");
            }
            else
            {
                ViewBag.ConsumerType = (int)model.MqMessageType;
            }
            string mqRequestUrl = ConfigHelper.GetAppsettingValue("MqRequestUrl");
            var mqRequestList = JsonConvert.DeserializeObject<List<MqRequestUrlResponse>>(mqRequestUrl);
            List<SelectListItem> selectList = new List<SelectListItem>() { new SelectListItem() { Text="请选择",Value="" } };
            mqRequestList.ForEach(a => {
                selectList.Add(new SelectListItem() { Text = a.InterfaceName, Value = a.Url });
            });
            ViewBag.MqRequestUrl = selectList;
            return View(model);
        }
        [MvcPowerFilter]
        public ActionResult Detail(MQBusinessInfoRequest request)
        {
            GetListItemByEnum();
            if (request?.ID <= 0)
            {
                return GoErrorPage("参数传递错误");
            }

            var viewModel = _mQBusinessConfigService.FindInfoById(request.ID);
            if (viewModel == null)
            {
                return GoErrorPage("任务信息不存在或已被删除");
            }
            return View(viewModel);
        }

        private void GetListItemByEnum()
        {
            ViewBag.MQFailMsgType = base.GetSelectListItemByEnum<MQFailMsgType>();
            ViewBag.MQConsumerType = base.GetSelectListItemByEnum<MQConsumerType>();
            ViewBag.StatusType = base.GetSelectListItemByEnum<CommonStatusType>();
        }
        #endregion

        #region 动作

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindList(MQBusinessRequest request)
        {
            var res = _mQBusinessConfigService.FindList(request);
            return Content(res.ToJsonString());
        }


        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddOrUpsert(MQBusinessAddOrUpsertRequest model)
        {
            var res = _mQBusinessConfigService.AddOrUpsert(model);
            return Content(res.ToJsonString());
        }
        #endregion
    }
}