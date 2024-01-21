using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Base;
using NIS.BLCore.Extensions;
using NIS.BLCore.Helpers;
using NIS.BLCore.Hubs;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.RequestStatusHistory;
using NIS.BLCore.Models.SignalR;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class RequestStatusHistoryLogic : BaseLogic<RequestStatusHistoryViewModel, RequestStatusHistoryCreateModel, RequestStatusHistoryFindModel, RequestStatusHistoryUpdateModel, RequestStatusHistoryDeleteModel>
    {
        #region Properties

        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        private readonly IHubContext<NisHub> _hubContext;
        public string[] OtherEntities = new[] { "CustomerRequest", "CustomerRequest.CreatedUser", "CustomerRequest.CreatedUser.Department" };
        private IConfiguration _configuration;

        #endregion

        #region Constructor

        public RequestStatusHistoryLogic(IConfiguration configuration, ClaimsPrincipal User, IHubContext<NisHub> hubContext) : base(User)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        #endregion


        #region RequestStatusHistory Add

        protected override void AddAbstract(RequestStatusHistoryCreateModel entity)
        {
            var output = new LogicResult<RequestStatusHistoryViewModel>();
            var data = Mapper.Map<RequestStatusHistoryCreateModel, RequestStatusHistory>(entity);
            data.UpdatedUserId = GeneralUserId;
            this.Uow.GetRepository<RequestStatusHistory>().Add(data);
            var saveData = this.Uow.SaveChanges();

            ///change Code
            if (saveData > 0)
            {
                var CustumerRequest = this.Uow.NisContext.CustomerRequest.FirstOrDefault(w => w.Id == data.CustomerRequestId);
                CustumerRequest.CreatedUser = this.Uow.NisContext.User.FirstOrDefault(f => f.Id == CustumerRequest.CreatedUserId);
                CustumerRequest.CreatedUser.Department = this.Uow.NisContext.Department.FirstOrDefault(f => f.Id == CustumerRequest.CreatedUser.DepartmentId);
                data.CustomerRequest = CustumerRequest;
            }
            ///change Code

            var request = this.Uow.GetRepository<CustomerRequest>().GetById(entity.CustomerRequestId);
            var mappedrequest = Mapper.Map<CustomerRequest, RequestViewModel>(request);
            mappedrequest.RequestStatusId = entity.RequestStatusId;

            if (entity.runSignalR)
            {
                //  Find relevant users for signalR

                var predicate = PredicateBuilder.True<User>();
                predicate = predicate.And(r => r.Id == data.CustomerRequest.CreatedUserId);

                predicate = predicate.Or(x => x.UserClaim.Any(a => a.ClaimValue == data.CustomerRequest.CreatedUser.Department.Alias));

                var at = this.Uow.GetRepository<User>().GetAll().Where(predicate).ToList();
                at = at.Where(a => a.RealTimeConnection.FirstOrDefault() != null).ToList();
                var users = at.Select(s => s.RealTimeConnection.LastOrDefault()).ToList();

                SignalRHelper.Remoterequestupdate(mappedrequest, users, _hubContext);

            }
            if (request.CreatedUserId == 294 ) ///////???????
            {
                var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                Task.Run(() =>
                {
                    var att = HttpClientExtentions.PostAsBasicAuth(_configuration, entity.CustomerRequestId, request.RequestStatusId);
                });
            }
            output.Output = Mapper.Map<RequestStatusHistory, RequestStatusHistoryViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RequestStatusHistoryCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RequestStatusHistoryViewModel>>();
            var data = Mapper.Map<List<RequestStatusHistoryCreateModel>, List<RequestStatusHistory>>(entities);
            data.ForEach(x => x.UpdatedUserId = GeneralUserId);
            var addedEntites = this.Uow.GetRepository<RequestStatusHistory>().AddRange(data);
            output.Output = Mapper.Map<List<RequestStatusHistory>, List<RequestStatusHistoryViewModel>>(addedEntites);
            ResultAll = output;
        }

        #endregion

        #region RequestStatusHistory Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RequestStatusHistoryViewModel>>();
            var data = this.Uow.GetRepository<RequestStatusHistory>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
               data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RequestStatusHistory>, ICollection<RequestStatusHistoryViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RequestStatusHistoryViewModel>();
            var data = this.Uow.GetRepository<RequestStatusHistory>().GetById(id);
            output.Output = Mapper.Map<RequestStatusHistory, RequestStatusHistoryViewModel>(data);
            Result = output;
        }

        #endregion

        #region RequestStatusHistory Find

        protected override void FindAbstract(RequestStatusHistoryFindModel parameters)
        {
            var output = new LogicResult<RequestStatusHistoryViewModel>();
            var predicate = PredicateBuilder.True<RequestStatusHistory>();
            if (parameters == null)
            {
                parameters = new RequestStatusHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => r.RequestStatusId == parameters.RequestStatusId);
            }
            if (parameters.UpdatedUserId != null)
            {
                predicate = predicate.And(r => r.UpdatedUserId == parameters.UpdatedUserId);
            }
            if (parameters.UpdatedDate != null)
            {
                predicate = predicate.And(r => r.UpdatedDate == parameters.UpdatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RequestStatusHistory>().Find(predicate);
            output.Output = Mapper.Map<RequestStatusHistory, RequestStatusHistoryViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RequestStatusHistoryFindModel parameters)
        {
            var output = new LogicResult<ICollection<RequestStatusHistoryViewModel>>();
            var predicate = PredicateBuilder.True<RequestStatusHistory>();
            if (parameters == null)
            {
                parameters = new RequestStatusHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => r.RequestStatusId == parameters.RequestStatusId);
            }
            if (parameters.UpdatedUserId != null)
            {
                predicate = predicate.And(r => r.UpdatedUserId == parameters.UpdatedUserId);
            }
            if (parameters.UpdatedDate != null)
            {
                predicate = predicate.And(r => r.UpdatedDate == parameters.UpdatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RequestStatusHistory>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RequestStatusHistory>, ICollection<RequestStatusHistoryViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region RequestStatusHistory Update

        protected override void UpdateAbstract(RequestStatusHistoryUpdateModel entity)
        {
            var output = new LogicResult<RequestStatusHistoryViewModel>();
            var curentRequestStatusHistory = this.Uow.GetRepository<RequestStatusHistory>().GetById(entity.Id);
            var requestStatusHistory = Mapper.Map(entity, curentRequestStatusHistory);
            var data = this.Uow.GetRepository<RequestStatusHistory>().Update(requestStatusHistory);
            output.Output = Mapper.Map<RequestStatusHistory, RequestStatusHistoryViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RequestStatusHistoryUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region RequestStatusHistory Remove

        protected override void RemoveAbstract(RequestStatusHistoryDeleteModel entity)
        {
            var output = new LogicResult<RequestStatusHistoryViewModel>();
            var data = Uow.GetRepository<RequestStatusHistory>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<RequestStatusHistory>().Remove(data);
            }
            else
            {
                output.ErrorList.Add(new Error
                {
                    Code = "400",
                    Text = "id is incorrect",
                    Type = UtilsCore.Enums.OperationResultCode.Information
                });
            }
            Result = output;
        }


        #endregion
    }
}
