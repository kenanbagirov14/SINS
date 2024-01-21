using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskHistory;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using Task = System.Threading.Tasks.Task;
using System.Web;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class TaskHistoryLogic : BaseLogic<TaskHistoryViewModel, TaskHistoryCreateModel, TaskHistoryFindModel, TaskHistoryUpdateModel, TaskHistoryDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TaskHistoryLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region TaskHistory Add

        protected override void AddAbstract(TaskHistoryCreateModel entity)
        {
            var output = new LogicResult<TaskHistoryViewModel>();
            var data = Mapper.Map<TaskHistoryCreateModel, TaskHistory>(entity);
            data.UpdatedUserId = GeneralUserId;
            Uow.GetRepository<TaskHistory>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<TaskHistory, TaskHistoryViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TaskHistoryCreateModel> parameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region TaskHistory Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TaskHistoryViewModel>>();
            var data = Uow.GetRepository<TaskHistory>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskHistory>, ICollection<TaskHistoryViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TaskHistoryViewModel>();
            var data = Uow.GetRepository<TaskHistory>().GetById(id);
            output.Output = Mapper.Map<TaskHistory, TaskHistoryViewModel>(data);
            Result = output;
        }

        #endregion

        #region TaskHistory Find

        protected override void FindAbstract(TaskHistoryFindModel parameters)
        {
            var output = new LogicResult<TaskHistoryViewModel>();
            var predicate = PredicateBuilder.True<TaskHistory>();
            if (parameters == null)
            {
                parameters = new TaskHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
            }
            if (parameters.Type != null)
            {
                predicate = predicate.And(r => r.Type == parameters.Type);
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
            var data = Uow.GetRepository<TaskHistory>().Find(predicate);
            output.Output = Mapper.Map<TaskHistory, TaskHistoryViewModel>(data);
        }

        protected override void FindAllAbstract(TaskHistoryFindModel parameters)
        {
            var output = new LogicResult<ICollection<TaskHistoryViewModel>>();
            var predicate = PredicateBuilder.True<TaskHistory>();
            if (parameters == null)
            {
                parameters = new TaskHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
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
            var data = Uow.GetRepository<TaskHistory>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskHistory>, ICollection<TaskHistoryViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region TaskHistory Update

        protected override void UpdateAbstract(TaskHistoryUpdateModel entity)
        {
            var output = new LogicResult<TaskHistoryViewModel>();
            var curentTaskHistory = Uow.GetRepository<TaskHistory>().GetById(entity.Id);
            var taskHistory = Mapper.Map(entity, curentTaskHistory);
            var data = Uow.GetRepository<TaskHistory>().Update(taskHistory);
            output.Output = Mapper.Map<TaskHistory, TaskHistoryViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TaskHistoryUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region TaskHistory Remove

        protected override void RemoveAbstract(TaskHistoryDeleteModel entity)
        {
            var output = new LogicResult<TaskHistoryViewModel>();
            var data = Uow.GetRepository<TaskHistory>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<TaskHistory>().Remove(data);
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
