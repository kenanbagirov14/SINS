using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.LawProcess;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class LawProcessLogic : BaseLogic<LawProcessViewModel, LawProcessCreateModel, LawProcessFindModel, LawProcessUpdateModel, LawProcessDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public LawProcessLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region LawProcess Add

        protected override void AddAbstract(LawProcessCreateModel parameter)
        {
            LogicResult<LawProcessViewModel> output = new LogicResult<LawProcessViewModel>();
            var data = Mapper.Map<LawProcessCreateModel, LawProcess>(parameter);
            this.Uow.GetRepository<LawProcess>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<LawProcess, LawProcessViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<LawProcessCreateModel> parameters)
        {
            LogicResult<ICollection<LawProcessViewModel>> output = new LogicResult<ICollection<LawProcessViewModel>>();
            var data = Mapper.Map<List<LawProcessCreateModel>, List<LawProcess>>(parameters);
            var addedEntities = Uow.GetRepository<LawProcess>().AddRange(data);
            output.Output = Mapper.Map<List<LawProcess>, List<LawProcessViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region LawProcess Get

        protected override void GetAllAbstract(Filter filter)
        {
            LogicResult<ICollection<LawProcessViewModel>> output = new LogicResult<ICollection<LawProcessViewModel>>();

            var data = this.Uow.GetRepository<LawProcess>().GetAll().ToList();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
                  data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            output.Output = Mapper.Map<List<LawProcess>, List<LawProcessViewModel>>(data).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            LogicResult<LawProcessViewModel> output = new LogicResult<LawProcessViewModel>();
            var data = this.Uow.GetRepository<LawProcess>().GetById(id);
            output.Output = Mapper.Map<LawProcess, LawProcessViewModel>(data);
            Result = output;
        }

        #endregion

        #region LawProcess Find

        protected override void FindAbstract(LawProcessFindModel parameters)
        {
            LogicResult<LawProcessViewModel> output = new LogicResult<LawProcessViewModel>();
            var predicate = PredicateBuilder.True<LawProcess>();
            if (parameters == null)
            {
                parameters = new LawProcessFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.OrderNo != null)
            {
                predicate = predicate.And(r => r.OrderNo.Contains(parameters.OrderNo));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<LawProcess>().Find(predicate);
            output.Output = Mapper.Map<LawProcess, LawProcessViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(LawProcessFindModel parameters)
        {
            LogicResult<ICollection<LawProcessViewModel>> output = new LogicResult<ICollection<LawProcessViewModel>>(); var predicate = PredicateBuilder.True<LawProcess>();
            if (parameters == null)
            {
                parameters = new LawProcessFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.OrderNo != null)
            {
                predicate = predicate.And(r => r.OrderNo.Contains(parameters.OrderNo));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<LawProcess>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<LawProcess>, ICollection<LawProcessViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region LawProcess Update

        protected override void UpdateAbstract(LawProcessUpdateModel parameter)
        {
            LogicResult<LawProcessViewModel> output = new LogicResult<LawProcessViewModel>();
            var curentLawProcess = this.Uow.GetRepository<LawProcess>().GetById(parameter.Id);
            var LawProcess = Mapper.Map(parameter, curentLawProcess);
            var data = this.Uow.GetRepository<LawProcess>().Update(LawProcess);
            output.Output = Mapper.Map<LawProcess, LawProcessViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(LawProcessUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LawProcess Remove

        protected override void RemoveAbstract(LawProcessDeleteModel entity)
        {
            LogicResult<LawProcessViewModel> output = new LogicResult<LawProcessViewModel>();
            var LawProcess = Uow.GetRepository<LawProcess>().GetById(entity.Id);
            this.Uow.GetRepository<LawProcess>().Remove(LawProcess);
            Result = output;
        }

        #endregion

    }
}
