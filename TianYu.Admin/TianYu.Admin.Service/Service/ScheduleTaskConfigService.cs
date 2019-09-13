using TianYu.Core.Common;
using TianYu.Core.Common.BaseEF;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain;
using TianYu.Admin.Domain.ViewModel;
using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Service.Service
{
    /// <summary>
    /// 任务设置接口
    /// </summary>
    public class ScheduleTaskConfigService : IScheduleTaskConfigService
    {
        private readonly IScheduleTaskConfigRepository _scheduleTaskConfigRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="scheduleTaskConfigRepository"></param>
        public ScheduleTaskConfigService(IScheduleTaskConfigRepository scheduleTaskConfigRepository)
        {
            this._scheduleTaskConfigRepository = scheduleTaskConfigRepository;
        }
        /// <summary>
        /// 获取所有任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BusinessPagedBaseViewModel<ScheduleTaskResponse> FindList(ScheduleTaskRequest request)
        {
            int totalCount = 0;
            var res = new BusinessPagedBaseViewModel<ScheduleTaskResponse>() { Status = ResponseStatus.Fail };

            #region 条件处理
            var filter = PredicateBuilder.True<ScheduleTaskConfig>();
            filter = filter.And(x => x.Status == (int)YesOrNo.Yes);
            if (request.CreateBeginTime.HasValue)
            {
                filter = filter.And(x => x.CreateTime >= request.CreateBeginTime.Value);
            }
            if (request.CreateEndTime.HasValue)
            {
                filter = filter.And(x => x.CreateTime <= request.CreateEndTime.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.KeyWords))
            {
                filter = filter.And(x => x.TaskName.Contains(request.KeyWords));
            }

            #endregion

            #region 排序处理
            var order = new IOrderByBuilder<ScheduleTaskConfig>[]
            {
                new OrderByBuilder<ScheduleTaskConfig, DateTime? >(x=>x.CreateTime, true),
            };
            #endregion

            var query = _scheduleTaskConfigRepository.Find(out totalCount, request.PageIndex, request.PageSize, filter, order);

            #region 返回数据处理
            res.Status = ResponseStatus.Success;
            var list = query.ToList();
            res.BusinessData = list.MapToList<ScheduleTaskResponse>();
            res.Total = totalCount;
            #endregion

            return res;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScheduleTaskResponse FindInfoById(int id)
        {
            var entity = new ScheduleTaskResponse();
            var query = _scheduleTaskConfigRepository.Find().FirstOrDefault(x => x.Id == id);
            if (query != null)
            {
                entity = query.MapTo<ScheduleTaskResponse>();
            }
            return entity;
        }

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BusinessBaseViewModel<int> AddOrUpsert(ScheduleTaskAddOrUpsertRequest request)
        {
            var errMsg = string.Empty;
            var insertId = 0;
            var res = new BusinessBaseViewModel<int>() { Status = ResponseStatus.Fail };

            #region 验证
            if (string.IsNullOrWhiteSpace(request.ApiUrl))
            {
                errMsg = "请求地址不能为空";
                goto error;
            }
            if (request.DiffSeconds == 0)
            {
                errMsg = "请求间隔时间必须大于0";
                goto error;
            }
            if (string.IsNullOrWhiteSpace(request.TaskName))
            {
                errMsg = "任务名称不能为空";
                goto error;
            }
            #endregion

            #region add or upsert
            if (request.Id > 0)
            {
                //upsert
                var model = _scheduleTaskConfigRepository.Find().FirstOrDefault(x => x.Id == request.Id);
                if (model != null)
                {
                    model.ApiUrl = request.ApiUrl;
                    model.DiffSeconds = request.DiffSeconds;
                    model.ExceptionNumber = request.ExceptionNumber;
                    model.ExecuteType = request.ExecuteType;
                    model.Method = request.Method;
                    model.Parameters = request.Parameters;
                    model.Status = request.Status;
                    model.TaskName = request.TaskName;
                    _scheduleTaskConfigRepository.Update(model);
                    goto success;
                }
            }
            else
            {
                //add
                var model = new ScheduleTaskConfig()
                {
                    TaskName = request.TaskName,
                    ApiUrl = request.ApiUrl,
                    CreateTime = DateTime.Now,
                    DiffSeconds = request.DiffSeconds,
                    ExceptionNumber = request.ExceptionNumber,
                    ExecuteType = request.ExecuteType,
                    Method = request.Method,
                    Parameters = string.IsNullOrWhiteSpace(request.Parameters) ? "" : request.Parameters,
                    RunStatus = request.RunStatus,
                    Status = request.Status,
                    LastRunTime = DateTime.Now
                };
                insertId = _scheduleTaskConfigRepository.Insert(model);
                goto success;
            }


        #endregion

        success:
            try
            {
                _scheduleTaskConfigRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            res.ErrorMessage = errMsg;
            res.Status = ResponseStatus.Success;
            res.BusinessData = insertId;
            return res;
        error:
            res.ErrorMessage = errMsg;
            return res;
        }
    }
}
