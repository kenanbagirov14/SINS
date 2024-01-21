using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.InjuryType;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class InjuryTypeLogic : BaseLogic<InjuryTypeViewModel, InjuryTypeCreateModel, InjuryTypeFindModel, InjuryTypeUpdateModel, InjuryTypeDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        //private readonly IHttpContextAccessor _accessor;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
 
        #endregion

        #region Constructor

        public InjuryTypeLogic(ClaimsPrincipal User ) : base(User)
        {

        }

        #endregion


        #region InjuryType Add

        protected override void AddAbstract(InjuryTypeCreateModel entity)
        {
            var output = new LogicResult<InjuryTypeViewModel>();
            var data = Mapper.Map<InjuryTypeCreateModel, InjuryType>(entity);
            this.Uow.GetRepository<InjuryType>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<InjuryType, InjuryTypeViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<InjuryTypeCreateModel> entities)
        {
            var output = new LogicResult<ICollection<InjuryTypeViewModel>>();
            var data = Mapper.Map<List<InjuryTypeCreateModel>, List<InjuryType>>(entities);
            var addedEntities = this.Uow.GetRepository<InjuryType>().AddRange(data);
            output.Output = Mapper.Map<List<InjuryType>, List<InjuryTypeViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region InjuryType Get

        protected override void GetAllAbstract(Filter filter)
        {
            var at = User;
            var output = new LogicResult<ICollection<InjuryTypeViewModel>>();
            var data = this.Uow.GetRepository<InjuryType>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<InjuryType>, ICollection<InjuryTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<InjuryTypeViewModel>();
            var data = this.Uow.GetRepository<InjuryType>().GetById(id);
            output.Output = Mapper.Map<InjuryType, InjuryTypeViewModel>(data);
            Result = output;
        }

        #endregion

        #region InjuryType Find

        protected override void FindAbstract(InjuryTypeFindModel parameters)
        {
            var output = new LogicResult<InjuryTypeViewModel>();
            var predicate = PredicateBuilder.True<InjuryType>();
            if (parameters == null)
            {
                parameters = new InjuryTypeFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => r.DepartmentId == parameters.DepartmentId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.ExecutionDay != null)
            {
                predicate = predicate.And(r => r.ExecutionDay == parameters.ExecutionDay);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<InjuryType>().Find(predicate);
            output.Output = Mapper.Map<InjuryType, InjuryTypeViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(InjuryTypeFindModel parameters)
        {
            var output = new LogicResult<ICollection<InjuryTypeViewModel>>();
            var predicate = PredicateBuilder.True<InjuryType>();
            if (parameters == null)
            {
                parameters = new InjuryTypeFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => r.DepartmentId == parameters.DepartmentId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.ExecutionDay != null)
            {
                predicate = predicate.And(r => r.ExecutionDay == parameters.ExecutionDay);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<InjuryType>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<InjuryType>, ICollection<InjuryTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region InjuryType Update

        protected override void UpdateAbstract(InjuryTypeUpdateModel entity)
        {
            var output = new LogicResult<InjuryTypeViewModel>();
            var curentInjuryType = this.Uow.GetRepository<InjuryType>().GetById(entity.Id);
            var injuryType = Mapper.Map(entity, curentInjuryType);
            var data = this.Uow.GetRepository<InjuryType>().Update(injuryType);
            output.Output = Mapper.Map<InjuryType, InjuryTypeViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(InjuryTypeUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region InjuryType Remove

        protected override void RemoveAbstract(InjuryTypeDeleteModel entity)
        {
            var output = new LogicResult<InjuryTypeViewModel>();
            var data = Uow.GetRepository<InjuryType>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<InjuryType>().Remove(data);
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
