using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.WorkFlow;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class WorkFlowLogic : BaseLogic<WorkFlowViewModel, WorkFlowCreateModel, WorkFlowFindModel, WorkFlowUpdateModel, WorkFlowDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public WorkFlowLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region WorkFlow Add

        protected override void AddAbstract(WorkFlowCreateModel entity)
        {
            var output = new LogicResult<WorkFlowViewModel>();
            var data = Mapper.Map<WorkFlowCreateModel, WorkFlow>(entity);
            this.Uow.GetRepository<WorkFlow>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<WorkFlow, WorkFlowViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<WorkFlowCreateModel> entities)
        {
            var output = new LogicResult<ICollection<WorkFlowViewModel>>();
            var data = Mapper.Map<List<WorkFlowCreateModel>, List<WorkFlow>>(entities);
            var addedEntities = this.Uow.GetRepository<WorkFlow>().AddRange(data);
            output.Output = Mapper.Map<List<WorkFlow>, List<WorkFlowViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region WorkFlow Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<WorkFlowViewModel>>();
            var data = this.Uow.GetRepository<WorkFlow>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<WorkFlow>, ICollection<WorkFlowViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<WorkFlowViewModel>();
            var data = this.Uow.GetRepository<WorkFlow>().GetById(id);
            output.Output = Mapper.Map<WorkFlow, WorkFlowViewModel>(data);
            Result = output;
        }

        #endregion

        #region WorkFlow Find

        protected override void FindAbstract(WorkFlowFindModel parameters)
        {
            var output = new LogicResult<WorkFlowViewModel>();
            var predicate = PredicateBuilder.True<WorkFlow>();
            if (parameters == null)
            {
                parameters = new WorkFlowFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }          
            var data = this.Uow.GetRepository<WorkFlow>().Find(predicate);
            output.Output = Mapper.Map<WorkFlow, WorkFlowViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(WorkFlowFindModel parameters)
        {
            var output = new LogicResult<ICollection<WorkFlowViewModel>>();
            var predicate = PredicateBuilder.True<WorkFlow>();
            if (parameters == null)
            {
                parameters = new WorkFlowFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
         
            var data = this.Uow.GetRepository<WorkFlow>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<WorkFlow>, ICollection<WorkFlowViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        #endregion

        #region WorkFlow Update

        protected override void UpdateAbstract(WorkFlowUpdateModel entity)
        {
            var output = new LogicResult<WorkFlowViewModel>();
            var curentWorkFlow = this.Uow.GetRepository<WorkFlow>().GetById(entity.Id);
            var WorkFlow = Mapper.Map(entity, curentWorkFlow);
            var data = this.Uow.GetRepository<WorkFlow>().Update(WorkFlow);
            output.Output = Mapper.Map<WorkFlow, WorkFlowViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(WorkFlowUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region WorkFlow Remove

        protected override void RemoveAbstract(WorkFlowDeleteModel entity)
        {
            var output = new LogicResult<WorkFlowViewModel>();
            var data = Uow.GetRepository<WorkFlow>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<WorkFlow>().Remove(data);
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
