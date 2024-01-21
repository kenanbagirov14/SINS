using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Region;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class RegionLogic : BaseLogic<RegionViewModel, RegionCreateModel, RegionFindModel, RegionUpdateModel, RegionDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public RegionLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Region Add

        protected override void AddAbstract(RegionCreateModel entity)
        {
            var output = new LogicResult<RegionViewModel>();
            var data = Mapper.Map<RegionCreateModel, Region>(entity);
            this.Uow.GetRepository<Region>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Region, RegionViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RegionCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RegionViewModel>>();
            var data = Mapper.Map<List<RegionCreateModel>, List<Region>>(entities);
            var addedEntities = this.Uow.GetRepository<Region>().AddRange(data);
            output.Output = Mapper.Map<List<Region>, List<RegionViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Region Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RegionViewModel>>();
            var data = this.Uow.GetRepository<Region>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Region>, ICollection<RegionViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RegionViewModel>();
            var data = this.Uow.GetRepository<Region>().GetById(id);
            output.Output = Mapper.Map<Region, RegionViewModel>(data);
            Result = output;
        }

        #endregion

        #region Region Find

        protected override void FindAbstract(RegionFindModel parameters)
        {
            var output = new LogicResult<RegionViewModel>();
            var predicate = PredicateBuilder.True<Region>();
            if (parameters == null)
            {
                parameters = new RegionFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.RegionPrefix != null)
            {
                predicate = predicate.And(r => r.RegionPrefix == parameters.RegionPrefix);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.EngineerId != null)
            {
                predicate = predicate.And(r => r.EngineerId == parameters.EngineerId);
            }
            var data = this.Uow.GetRepository<Region>().Find(predicate);
            output.Output = Mapper.Map<Region, RegionViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RegionFindModel parameters)
        {
            var output = new LogicResult<ICollection<RegionViewModel>>();
            var predicate = PredicateBuilder.True<Region>();
            if (parameters == null)
            {
                parameters = new RegionFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.RegionPrefix != null)
            {
                predicate = predicate.And(r => r.RegionPrefix == parameters.RegionPrefix);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.EngineerId != null)
            {
                predicate = predicate.And(r => r.EngineerId == parameters.EngineerId);
            }
            var data = this.Uow.GetRepository<Region>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Region>, ICollection<RegionViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region Region Update

        protected override void UpdateAbstract(RegionUpdateModel entity)
        {
            var output = new LogicResult<RegionViewModel>();
            var curentRegion = this.Uow.GetRepository<Region>().GetById(entity.Id);
            var region = Mapper.Map(entity, curentRegion);
            var data = this.Uow.GetRepository<Region>().Update(region);
            output.Output = Mapper.Map<Region, RegionViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RegionUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Region Remove

        protected override void RemoveAbstract(RegionDeleteModel entity)
        {
            var output = new LogicResult<RegionViewModel>();
            var data = Uow.GetRepository<Region>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Region>().Remove(data);
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
