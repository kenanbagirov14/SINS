using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskStatus;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using TaskStatus = NIS.DALCore.Context.TaskStatus;

namespace NIS.BLCore.Logics
{
    public class TaskStatusLogic : BaseLogic<TaskStatusViewModel, TaskStatusCreateModel, TaskStatusFindModel, TaskStatusUpdateModel, TaskStatusDeleteModel>
    {
        #region Properties
        
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TaskStatusLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region TaskStatus Add

        protected override void AddAbstract(TaskStatusCreateModel entity)
        {
            var output = new LogicResult<TaskStatusViewModel>();
            var data = Mapper.Map<TaskStatusCreateModel, TaskStatus>(entity);
            var result = this.Uow.GetRepository<TaskStatus>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<TaskStatus, TaskStatusViewModel>(result);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TaskStatusCreateModel> parameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region TaskStatus Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TaskStatusViewModel>>();
            var data = this.Uow.GetRepository<TaskStatus>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskStatus>, ICollection<TaskStatusViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TaskStatusViewModel>();
            var data = this.Uow.GetRepository<TaskStatus>().GetById(id);
            output.Output = Mapper.Map<TaskStatus, TaskStatusViewModel>(data);
            Result = output;
        }

        #endregion

        #region TaskStatus Find

        protected override void FindAbstract(TaskStatusFindModel parameters)
        {
            var output = new LogicResult<TaskStatusViewModel>();
            var predicate = PredicateBuilder.True<TaskStatus>();
            if (parameters == null)
            {
                parameters = new TaskStatusFindModel();
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
            var data = this.Uow.GetRepository<TaskStatus>().Find(predicate);
            output.Output = Mapper.Map<TaskStatus, TaskStatusViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(TaskStatusFindModel parameters)
        {
            var output = new LogicResult<ICollection<TaskStatusViewModel>>();
            var predicate = PredicateBuilder.True<TaskStatus>();
            if (parameters == null)
            {
                parameters = new TaskStatusFindModel();
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
            var data = this.Uow.GetRepository<TaskStatus>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskStatus>, ICollection<TaskStatusViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region TaskStatus Update

        protected override void UpdateAbstract(TaskStatusUpdateModel entity)
        {
            var output = new LogicResult<TaskStatusViewModel>();
            var curentTaskStatus = this.Uow.GetRepository<TaskStatus>().GetById(entity.Id);
            var taskStatus = Mapper.Map(entity, curentTaskStatus);
            var data = this.Uow.GetRepository<TaskStatus>().Update(taskStatus);
            output.Output = Mapper.Map<TaskStatus, TaskStatusViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TaskStatusUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region TaskStatus Remove

        protected override void RemoveAbstract(TaskStatusDeleteModel entity)
        {
            var output = new LogicResult<TaskStatusViewModel>();
            var data = Uow.GetRepository<TaskStatus>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<TaskStatus>().Remove(data);
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
