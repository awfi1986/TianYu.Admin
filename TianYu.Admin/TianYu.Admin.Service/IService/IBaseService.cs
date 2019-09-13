using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Admin.Service.IService
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessPagedBaseViewModel<T> Query(T request);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<T> QueryDetail(T request);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Insert(T request);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Update(T request);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BusinessBaseViewModel<string> Remove(T request);
    }
}
