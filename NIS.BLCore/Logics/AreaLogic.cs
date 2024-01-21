using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Area;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class AreaLogic : BaseLogic<AreaViewModel, AreaCreateModel, AreaFindModel, AreaUpdateModel, AreaDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public AreaLogic(ClaimsPrincipal User) : base(User)
        { 
        }

        #endregion


        #region Area Add

        protected override void AddAbstract(AreaCreateModel parameter)
        {
            LogicResult<AreaViewModel> output = new LogicResult<AreaViewModel>();
            var data = Mapper.Map<AreaCreateModel, Area>(parameter);
            this.Uow.GetRepository<Area>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Area, AreaViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<AreaCreateModel> parameters)
        {
            LogicResult<ICollection<AreaViewModel>> output = new LogicResult<ICollection<AreaViewModel>>();
            var data = Mapper.Map<List<AreaCreateModel>, List<Area>>(parameters);
            var addedEntities = Uow.GetRepository<Area>().AddRange(data);
            output.Output = Mapper.Map<List<Area>, List<AreaViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Area Get

        protected override void GetAllAbstract(Filter filter)
        { 
            LogicResult<ICollection<AreaViewModel>> output = new LogicResult<ICollection<AreaViewModel>>();

            var data = this.Uow.GetRepository<Area>().GetAll().ToList();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
                  data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            output.Output = Mapper.Map<List<Area>, List<AreaViewModel>>(data).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            LogicResult<AreaViewModel> output = new LogicResult<AreaViewModel>();
            var data = this.Uow.GetRepository<Area>().GetById(id);
            output.Output = Mapper.Map<Area, AreaViewModel>(data);
            Result = output;
        }

        #endregion

        #region Area Find

        protected override void FindAbstract(AreaFindModel parameters)
        {
            LogicResult<AreaViewModel> output = new LogicResult<AreaViewModel>();
            var predicate = PredicateBuilder.True<Area>();
            if (parameters == null)
            {
                parameters = new AreaFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => r.RegionId == parameters.RegionId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.AreaPrefix != null)
            {
                predicate = predicate.And(r => r.AreaPrefix == parameters.AreaPrefix);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Area>().Find(predicate);
            output.Output = Mapper.Map<Area, AreaViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(AreaFindModel parameters)
        {
            LogicResult<ICollection<AreaViewModel>> output = new LogicResult<ICollection<AreaViewModel>>(); var predicate = PredicateBuilder.True<Area>();
            if (parameters == null)
            {
                parameters = new AreaFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => r.RegionId == parameters.RegionId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.AreaPrefix != null)
            {
                predicate = predicate.And(r => r.AreaPrefix == parameters.AreaPrefix);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Area>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Area>, ICollection<AreaViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Area Update

        protected override void UpdateAbstract(AreaUpdateModel parameter)
        {
            LogicResult<AreaViewModel> output = new LogicResult<AreaViewModel>();
            var curentArea = this.Uow.GetRepository<Area>().GetById(parameter.Id);
            var area = Mapper.Map(parameter, curentArea);
            var data = this.Uow.GetRepository<Area>().Update(area);
            output.Output = Mapper.Map<Area, AreaViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(AreaUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Area Remove

        protected override void RemoveAbstract(AreaDeleteModel entity)
        {
            LogicResult<AreaViewModel> output = new LogicResult<AreaViewModel>();
            var area = Uow.GetRepository<Area>().GetById(entity.Id);
            this.Uow.GetRepository<Area>().Remove(area);
            Result = output;
        }

        #endregion

    }
}
