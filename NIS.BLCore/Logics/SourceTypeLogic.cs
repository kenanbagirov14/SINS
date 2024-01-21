using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.SourceType;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class SourceTypeLogic : BaseLogic<SourceTypeViewModel, SourceTypeCreateModel, SourceTypeFindModel, SourceTypeUpdateModel, SourceTypeDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public SourceTypeLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region SourceType Add

        protected override void AddAbstract(SourceTypeCreateModel entity)
        {
            var output = new LogicResult<SourceTypeViewModel>();
            var data = Mapper.Map<SourceTypeCreateModel, SourceType>(entity);
            this.Uow.GetRepository<SourceType>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<SourceType, SourceTypeViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<SourceTypeCreateModel> entities)
        {
            var output = new LogicResult<ICollection<SourceTypeViewModel>>();
            var data = Mapper.Map<List<SourceTypeCreateModel>, List<SourceType>>(entities);
            var addedEntities = this.Uow.GetRepository<SourceType>().AddRange(data);
            output.Output = Mapper.Map<List<SourceType>, List<SourceTypeViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region SourceType Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<SourceTypeViewModel>>();
            var data = this.Uow.GetRepository<SourceType>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<SourceType>, ICollection<SourceTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<SourceTypeViewModel>();
            var data = this.Uow.GetRepository<SourceType>().GetById(id);
            output.Output = Mapper.Map<SourceType, SourceTypeViewModel>(data);
            Result = output;
        }

        #endregion

        #region SourceType Find

        protected override void FindAbstract(SourceTypeFindModel parameters)
        {
            var output = new LogicResult<SourceTypeViewModel>();
            var predicate = PredicateBuilder.True<SourceType>();
            if (parameters == null)
            {
                parameters = new SourceTypeFindModel();
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
            var data = this.Uow.GetRepository<SourceType>().Find(predicate);
            output.Output = Mapper.Map<SourceType, SourceTypeViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(SourceTypeFindModel parameters)
        {
            var output = new LogicResult<ICollection<SourceTypeViewModel>>();
            var predicate = PredicateBuilder.True<SourceType>();
            if (parameters == null)
            {
                parameters = new SourceTypeFindModel();
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
            var data = this.Uow.GetRepository<SourceType>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<SourceType>, ICollection<SourceTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region SourceType Update

        protected override void UpdateAbstract(SourceTypeUpdateModel entity)
        {
            var output = new LogicResult<SourceTypeViewModel>();
            var curentSourceType = this.Uow.GetRepository<SourceType>().GetById(entity.Id);
            var sourceType = Mapper.Map(entity, curentSourceType);
            var data = this.Uow.GetRepository<SourceType>().Update(sourceType);
            output.Output = Mapper.Map<SourceType, SourceTypeViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(SourceTypeUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region SourceType Remove

        protected override void RemoveAbstract(SourceTypeDeleteModel entity)
        {
            var output = new LogicResult<SourceTypeViewModel>();
            var data = Uow.GetRepository<SourceType>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<SourceType>().Remove(data);
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
