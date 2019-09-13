
using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain.ViewModel;

namespace TianYu.Admin.Service.IService
{
    public partial interface IMQBusinessConfigService
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessPagedBaseViewModel<MQBusinessResponse> FindList(MQBusinessRequest request);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<int> AddOrUpsert(MQBusinessAddOrUpsertRequest request);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MQBusinessResponse FindInfoById(int id);

    }
}