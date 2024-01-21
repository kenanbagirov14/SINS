using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RequestStatus;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class RequestStatusLogic : BaseLogic<RequestStatusViewModel, RequestStatusCreateModel, RequestStatusFindModel, RequestStatusUpdateModel, RequestStatusDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public RequestStatusLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region RequestStatus Add

        protected override void AddAbstract(RequestStatusCreateModel entity)
        {
            var output = new LogicResult<RequestStatusViewModel>();
            var data = Mapper.Map<RequestStatusCreateModel, RequestStatus>(entity);
            this.Uow.GetRepository<RequestStatus>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<RequestStatus, RequestStatusViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RequestStatusCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RequestStatusViewModel>>();
            var data = Mapper.Map<List<RequestStatusCreateModel>, List<RequestStatus>>(entities);
            var addedEntities = this.Uow.GetRepository<RequestStatus>().AddRange(data);
            output.Output = Mapper.Map<List<RequestStatus>, List<RequestStatusViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region RequestStatus Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RequestStatusViewModel>>();
            var data = this.Uow.GetRepository<RequestStatus>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RequestStatus>, ICollection<RequestStatusViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RequestStatusViewModel>();
            var data = this.Uow.GetRepository<RequestStatus>().GetById(id);
            output.Output = Mapper.Map<RequestStatus, RequestStatusViewModel>(data);
            Result = output;
        }

        #endregion

        #region RequestStatus Find

        protected override void FindAbstract(RequestStatusFindModel parameters)
        {
            var output = new LogicResult<RequestStatusViewModel>();
            var predicate = PredicateBuilder.True<RequestStatus>();
            if (parameters == null)
            {
                parameters = new RequestStatusFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RequestStatus>().Find(predicate);
            output.Output = Mapper.Map<RequestStatus, RequestStatusViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RequestStatusFindModel parameters)
        {
            var output = new LogicResult<ICollection<RequestStatusViewModel>>();
            var predicate = PredicateBuilder.True<RequestStatus>();
            if (parameters == null)
            {
                parameters = new RequestStatusFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RequestStatus>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RequestStatus>, ICollection<RequestStatusViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region RequestStatus Update

        protected override void UpdateAbstract(RequestStatusUpdateModel entity)
        {
            var output = new LogicResult<RequestStatusViewModel>();
            var curentRequestStatus = this.Uow.GetRepository<RequestStatus>().GetById(entity.Id);
            var requestStatus = Mapper.Map(entity, curentRequestStatus);
            var data = this.Uow.GetRepository<RequestStatus>().Update(requestStatus);
            output.Output = Mapper.Map<RequestStatus, RequestStatusViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RequestStatusUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region RequestStatus Remove

        protected override void RemoveAbstract(RequestStatusDeleteModel entity)
        {
            var output = new LogicResult<RequestStatusViewModel>();
            var data = Uow.GetRepository<RequestStatus>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<RequestStatus>().Remove(data);
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
