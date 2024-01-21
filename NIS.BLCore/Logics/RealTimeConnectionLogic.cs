using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RealTimeConnection;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class RealTimeConnectionLogic : BaseLogic<RealTimeConnectionViewModel, RealTimeConnectionCreateModel, RealTimeConnectionFindModel, RealTimeConnectionUpdateModel, RealTimeConnectionDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public RealTimeConnectionLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region RealTimeConnection Add

        protected override void AddAbstract(RealTimeConnectionCreateModel entity)
        {
            var output = new LogicResult<RealTimeConnectionViewModel>();
            var data = Mapper.Map<RealTimeConnectionCreateModel, RealTimeConnection>(entity);
            this.Uow.GetRepository<RealTimeConnection>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<RealTimeConnection, RealTimeConnectionViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RealTimeConnectionCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RealTimeConnectionViewModel>>();
            var data = Mapper.Map<List<RealTimeConnectionCreateModel>, List<RealTimeConnection>>(entities);
            var addedEntities = this.Uow.GetRepository<RealTimeConnection>().AddRange(data);
            output.Output = Mapper.Map<List<RealTimeConnection>, List<RealTimeConnectionViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region RealTimeConnection Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RealTimeConnectionViewModel>>();
            var data = this.Uow.GetRepository<RealTimeConnection>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.TotalCount = data.Count();
            output.Output = Mapper.Map<ICollection<RealTimeConnection>, ICollection<RealTimeConnectionViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RealTimeConnectionViewModel>();
            var data = this.Uow.GetRepository<RealTimeConnection>().GetById(id);
            output.Output = Mapper.Map<RealTimeConnection, RealTimeConnectionViewModel>(data);
            Result = output;
        }

        #endregion

        #region RealTimeConnection Find

        protected override void FindAbstract(RealTimeConnectionFindModel parameters)
        {
            var output = new LogicResult<RealTimeConnectionViewModel>();
            var predicate = PredicateBuilder.True<RealTimeConnection>();
            if (parameters == null)
            {
                parameters = new RealTimeConnectionFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ConnectionId != null)
            {
                predicate = predicate.And(r => r.ConnectionId == parameters.ConnectionId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RealTimeConnection>().Find(predicate);
            output.Output = Mapper.Map<RealTimeConnection, RealTimeConnectionViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RealTimeConnectionFindModel parameters)
        {
            var output = new LogicResult<ICollection<RealTimeConnectionViewModel>>();
            var predicate = PredicateBuilder.True<RealTimeConnection>();
            if (parameters == null)
            {
                parameters = new RealTimeConnectionFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ConnectionId != null)
            {
                predicate = predicate.And(r => r.ConnectionId == parameters.ConnectionId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<RealTimeConnection>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RealTimeConnection>, ICollection<RealTimeConnectionViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region RealTimeConnection Update

        protected override void UpdateAbstract(RealTimeConnectionUpdateModel entity)
        {
            var output = new LogicResult<RealTimeConnectionViewModel>();
            var curentRealTimeConnection = this.Uow.GetRepository<RealTimeConnection>().GetById(entity.Id);
            var injuryType = Mapper.Map(entity, curentRealTimeConnection);
            var data = this.Uow.GetRepository<RealTimeConnection>().Update(injuryType);
            output.Output = Mapper.Map<RealTimeConnection, RealTimeConnectionViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RealTimeConnectionUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region RealTimeConnection Remove

        protected override void RemoveAbstract(RealTimeConnectionDeleteModel entity)
        {
            var output = new LogicResult<RealTimeConnectionViewModel>();
            var data = Uow.GetRepository<RealTimeConnection>().GetAll()
              .FirstOrDefault(x => x.ConnectionId == entity.ConnectionId);
            if (data != null)
            {
                Uow.GetRepository<RealTimeConnection>().Remove(data);
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


