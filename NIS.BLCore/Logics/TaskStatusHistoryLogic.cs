using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskStatusHistory;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using NIS.UtilsCore.Enums;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace NIS.BLCore.Logics
{
    public class TaskStatusHistoryLogic : BaseLogic<TaskStatusHistoryViewModel, TaskStatusHistoryCreateModel, TaskStatusHistoryFindModel, TaskStatusHistoryUpdateModel, TaskStatusHistoryDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        private IConfiguration _configuration;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TaskStatusHistoryLogic(IConfiguration configuration, ClaimsPrincipal User) : base(User)
        {
            _configuration = configuration;
        }

        #endregion


        #region TaskStatusHistory Add

        protected override void AddAbstract(TaskStatusHistoryCreateModel entity)
        {
            var output = new LogicResult<TaskStatusHistoryViewModel>();
            var data = Mapper.Map<TaskStatusHistoryCreateModel, TaskStatusHistory>(entity);
            data.UpdatedUserId = GeneralUserId;
            var result = this.Uow.GetRepository<TaskStatusHistory>().Add(data);
            Uow.SaveChanges();
            var task = this.Uow.GetRepository<MainTask>().GetById(entity.MainTaskId);
            if (entity.TaskStatusId == (int)TaskStatusEnum.IcraOlundu)
            {
                var request = this.Uow.GetRepository<CustomerRequest>().GetAll().FirstOrDefault(x => x.MainTask.FirstOrDefault(a => a.Id == entity.MainTaskId) != null).MainTask.All(s => s.TaskStatusId == (int)TaskStatusEnum.IcraOlundu);
                if (request)
                {
                    Task.Run(() =>
                    {
                        var at = HttpClientExtentions.PostAsBasicAuth(_configuration,task.CustomerRequestId, (int)RequestStatusEnum.IcraOlundu);
                    });
                }
            }
            output.Output = Mapper.Map<TaskStatusHistory, TaskStatusHistoryViewModel>(result);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TaskStatusHistoryCreateModel> parameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region TaskStatusHistory Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TaskStatusHistoryViewModel>>();
            var data = this.Uow.GetRepository<TaskStatusHistory>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskStatusHistory>, ICollection<TaskStatusHistoryViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TaskStatusHistoryViewModel>();
            var data = this.Uow.GetRepository<TaskStatusHistory>().GetById(id);
            output.Output = Mapper.Map<TaskStatusHistory, TaskStatusHistoryViewModel>(data);
            Result = output;
        }

        #endregion

        #region TaskStatusHistory Find

        protected override void FindAbstract(TaskStatusHistoryFindModel parameters)
        {
            var output = new LogicResult<TaskStatusHistoryViewModel>();
            var predicate = PredicateBuilder.True<TaskStatusHistory>();
            if (parameters == null)
            {
                parameters = new TaskStatusHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.TaskStatusId != null)
            {
                predicate = predicate.And(r => r.TaskStatusId == parameters.TaskStatusId);
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
            var data = this.Uow.GetRepository<TaskStatusHistory>().Find(predicate);
            output.Output = Mapper.Map<TaskStatusHistory, TaskStatusHistoryViewModel>(data);
        }

        protected override void FindAllAbstract(TaskStatusHistoryFindModel parameters)
        {
            var output = new LogicResult<ICollection<TaskStatusHistoryViewModel>>();
            var predicate = PredicateBuilder.True<TaskStatusHistory>();
            if (parameters == null)
            {
                parameters = new TaskStatusHistoryFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.TaskStatusId != null)
            {
                predicate = predicate.And(r => r.TaskStatusId == parameters.TaskStatusId);
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
            var data = this.Uow.GetRepository<TaskStatusHistory>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TaskStatusHistory>, ICollection<TaskStatusHistoryViewModel>>(data.ToList());
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region TaskStatusHistory Update

        protected override void UpdateAbstract(TaskStatusHistoryUpdateModel entity)
        {
            var output = new LogicResult<TaskStatusHistoryViewModel>();
            var curentTaskStatusHistory = this.Uow.GetRepository<TaskStatusHistory>().GetById(entity.Id);
            var taskStatusHistory = Mapper.Map(entity, curentTaskStatusHistory);
            var data = this.Uow.GetRepository<TaskStatusHistory>().Update(taskStatusHistory);
            output.Output = Mapper.Map<TaskStatusHistory, TaskStatusHistoryViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TaskStatusHistoryUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }


        #endregion

        #region TaskStatusHistory Remove

        protected override void RemoveAbstract(TaskStatusHistoryDeleteModel entity)
        {
            var output = new LogicResult<TaskStatusHistoryViewModel>();
            var data = Uow.GetRepository<TaskStatusHistory>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<TaskStatusHistory>().Remove(data);
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
