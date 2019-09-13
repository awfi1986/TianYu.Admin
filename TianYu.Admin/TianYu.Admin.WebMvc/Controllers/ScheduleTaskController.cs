using TianYu.Core.Common; 
using TianYu.Admin.Domain.ViewModel;
using TianYu.Admin.Infrastructure.Enum;
using TianYu.Admin.Service.IService; 
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Api;

namespace TianYu.Admin.WebMvc.Controllers
{
    [ValidateLogin]
    public class ScheduleTaskController : BaseController
    {
        #region 变量
        /// <summary>
        /// 业务逻辑服务接口
        /// </summary>
        private IScheduleTaskConfigService _scheduleTaskConfigService;
        #endregion

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="scheduleTaskConfigService"></param>
        public ScheduleTaskController(IScheduleTaskConfigService scheduleTaskConfigService)
        {
            this._scheduleTaskConfigService = scheduleTaskConfigService;
        }
        #endregion

        #region 视图
        /// <summary>
        /// index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            GetListItemByEnum();
            return View();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns> 
        public ActionResult Adds()
        {
            ScheduleTaskResponse model = new ScheduleTaskResponse();
            GetListItemByEnum();
            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns> 
        public ActionResult Edits(ScheduleTaskInfoRequest request)
        {
            GetListItemByEnum();
            var model = _scheduleTaskConfigService.FindInfoById(request.Id);
            if (model == null)
            {
                return GoErrorPage("定时任务信息不存在或已被删除");
            }
            return View(model);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        public ActionResult Detail(ScheduleTaskInfoRequest request)
        {
            GetListItemByEnum();
            if (request?.Id <= 0)
            {
                return GoErrorPage("参数传递错误");
            }

            var viewModel = _scheduleTaskConfigService.FindInfoById(request.Id);
            if (viewModel == null)
            {
                return GoErrorPage("定时任务信息不存在或已被删除");
            }
            return View(viewModel);
        }

        private void GetListItemByEnum()
        {
            ViewBag.TaskStatusType = base.GetSelectListItemByEnum<CommonStatusType>(false);
            ViewBag.TaskExecuteType = base.GetSelectListItemByEnum<TaskExecuteType>(false);
        }
        #endregion

        #region 动作
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindList(ScheduleTaskRequest request)
        {
            var res = _scheduleTaskConfigService.FindList(request);
            return Content(res.ToJsonString());
        }


        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddOrUpsert(ScheduleTaskAddOrUpsertRequest model)
        {
            var res = _scheduleTaskConfigService.AddOrUpsert(model);
            return Content(res.ToJsonString());
        }
        #endregion

    }
}