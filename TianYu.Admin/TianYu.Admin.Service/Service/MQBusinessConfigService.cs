using TianYu.Core.Common;
using TianYu.Core.Common.BaseEF;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Admin.Domain;
using TianYu.Admin.Domain.ViewModel;
using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using System;
using System.Linq;

namespace TianYu.Admin.Service.Service
{
    public class MQBusinessConfigService : BaseService, IMQBusinessConfigService
    {
        private readonly IMQBusinessConfigRepository _mQBusinessConfigRepository;
        public MQBusinessConfigService(IMQBusinessConfigRepository mQBusinessConfigRepository)
        {
            this._mQBusinessConfigRepository = mQBusinessConfigRepository;
        }

        public BusinessBaseViewModel<int> AddOrUpsert(MQBusinessAddOrUpsertRequest request)
        {
            var errMsg = string.Empty;
            var insertId = 0;
            var res = new BusinessBaseViewModel<int>() { Status = ResponseStatus.Fail };
            #region 验证
            if (string.IsNullOrWhiteSpace(request.ApiUrl))
            {
                errMsg = "地址不能为空";
                goto error;
            }

            if (string.IsNullOrWhiteSpace(request.BusinessName))
            {
                errMsg = "任务名称不能为空";
                goto error;
            }
            if (string.IsNullOrWhiteSpace(request.QueueName))
            {
                errMsg = "队列名称不能为空";
                goto error;
            }
            #endregion

            #region add or upsert

            if (request.Id > 0)
            {
                //upsert
                var model = _mQBusinessConfigRepository.Find().FirstOrDefault(x => x.Id == request.Id);
                if (model != null)
                {
                    model.ApiUrl = request.ApiUrl;
                    model.BusinessName = request.BusinessName;
                    model.ExceptionNumber = request.ExceptionNumber;
                    model.Exchange = request.Exchange;
                    model.IsProperties = request.IsProperties;
                    model.IsSaveFailMessage = request.IsSaveFailMessage;
                    model.Status = request.Status;
                    model.MqMessageType = request.MqMessageType;
                    model.QueueName = request.QueueName;
                    model.RoutingKey = request.RoutingKey;
                    model.TimeInterval = request.TimeInterval;
                    _mQBusinessConfigRepository.Update(model);
                    goto success;
                }
            }
            else
            {
                //add
                var model = new MQBusinessConfig()
                {
                    BusinessName = request.BusinessName,
                    ApiUrl = request.ApiUrl,
                    CreateTime = DateTime.Now,
                    ExceptionNumber = request.ExceptionNumber,
                    Status = (int)request.Status,
                    Exchange = request.Exchange,
                    TimeInterval = request.TimeInterval,
                    RoutingKey = request.RoutingKey,
                    QueueName = request.QueueName,
                    MqMessageType = request.MqMessageType,
                    IsSaveFailMessage = request.IsSaveFailMessage,
                    IsProperties = request.IsProperties
                };
                insertId = _mQBusinessConfigRepository.Insert(model);
                goto success;
            }


        #endregion

        success:
            try
            {
                _mQBusinessConfigRepository.SaveChanges();
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

        public MQBusinessResponse FindInfoById(int id)
        {
            var entity = new MQBusinessResponse();
            var query = _mQBusinessConfigRepository.Find().FirstOrDefault(x => x.Id == id);
            if (query != null)
            {
                entity = query.MapTo<MQBusinessResponse>();
            }
            return entity;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BusinessPagedBaseViewModel<MQBusinessResponse> FindList(MQBusinessRequest request)
        {
            int totalCount = 0;
            var res = new BusinessPagedBaseViewModel<MQBusinessResponse>() { Status = ResponseStatus.Fail };

            #region 条件处理
            var filter = PredicateBuilder.True<MQBusinessConfig>();
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
                filter = filter.And(x => x.BusinessName.Contains(request.KeyWords));
            }

            #endregion

            #region 排序处理
            var order = new IOrderByBuilder<MQBusinessConfig>[]
            {
                new OrderByBuilder<MQBusinessConfig, DateTime? >(x=>x.CreateTime, true),
            };
            #endregion

            var query = _mQBusinessConfigRepository.Find(out totalCount, request.PageIndex, request.PageSize, filter, order);

            #region 返回数据处理
            res.Status = ResponseStatus.Success;
            var list = query.ToList();
            res.BusinessData = list.MapToList<MQBusinessResponse>();
            res.Total = totalCount;
            #endregion

            return res;
        }
    }
}
