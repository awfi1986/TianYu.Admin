using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain;
using TianYu.Admin.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Service.IService
{
    public partial interface IScheduleTaskConfigService
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessPagedBaseViewModel<ScheduleTaskResponse> FindList(ScheduleTaskRequest request);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<int> AddOrUpsert(ScheduleTaskAddOrUpsertRequest request);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ScheduleTaskResponse FindInfoById(int id);
    }
}
