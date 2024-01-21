using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Base;
using NIS.BLCore.Extensions;
using NIS.BLCore.Helpers;
using NIS.BLCore.Hubs;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;

namespace NIS.BLCore.Logics
{
    public class MainTaskLogic : BaseLogic<TaskViewModel, TaskCreateModel, TaskFindModel, TaskUpdateModel, TaskDeleteModel>
    {
        #region Properties

        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        public string[] OtherEntities = new[] { "CustomerRequest", "Attachment", "ExecutorUser", "InjuryType", "RealInjuryType", "TaskStatus", "Tag", "Rating", "ParentTask", "ChildTasks" };
        // public string[] OtherEntitiesUpdate = new[] {  };
        public string[] OtherEntitiesAdd = new[] { "Attachment", "ChildTasks", "Department" };
        private readonly IHubContext<NisHub> _hubContext;
        private IConfiguration _configuration;


        #endregion

        #region Constructor

        public MainTaskLogic(IConfiguration configuration, ClaimsPrincipal User, IHubContext<NisHub> hubContext) : base(User)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        #endregion


        #region Task Add

        protected override void AddAbstract(TaskCreateModel entity)
        {
            var output = new LogicResult<TaskViewModel>();

            if (DataHolder.DailyInsertedPhones(entity.Description))
            {
                output.Output = Mapper.Map<TaskCreateModel, TaskViewModel>(entity);
                output.ErrorList.Add(new Error() { Code = "408", Type = OperationResultCode.Error, Text = "Task already exist" });
                Result = output;
                return;
            }
            else
            {
                //same codes DataHolder.DailyInsertedPhones(entity.Description, true);
            }

            var data = Mapper.Map<TaskCreateModel, MainTask>(entity);
            data.GeneratedUserId = entity.GeneratedUserId ?? GeneralUserId;
            data.UpdatedUserId = GeneralUserId;
            data.TaskStatusId = (int)TaskStatusEnum.Yeni;
            this.Uow.GetRepository<MainTask>().Add(data);

            //"Attachment", "ChildTasks", "Department"

            var dff = data;
            int mm = this.Uow.SaveChanges();
            if (mm > 0)
            {
                var _Department = this.Uow.NisContext.Department.FirstOrDefault(f => f.Id == data.DepartmentId);
                var _DepMAinTask = this.Uow.NisContext.MainTask.Where(ff => ff.DepartmentId == data.DepartmentId);
                var _ChildTask = this.Uow.NisContext.MainTask.Where(f => f.MainTaskId == data.Id);

                _Department.MainTask = _DepMAinTask.ToList();
                data.Department = _Department;
                data.ChildTasks = _ChildTask.ToList();

                //data = this.Uow.NisContext.MainTask
                //   .Include("Attachment")
                //   .FirstOrDefault(f => f.Id == data.Id);
            }



            if (mm > 0)
            {
                if (entity.Description.Contains("Azercell"))
                {
                    DataHolder.DailyInsertedPhones(entity.CustomerRequestId.ToString() + entity.Description, true);
                }
                else
                {
                    DataHolder.DailyInsertedPhones(entity.Description, true);
                }
            }
            Task.Run(() =>
              {
                  //  Find relevant users for signalR

                  var users1 = this.Uow.NisContext.User
                        .Where(x => x.Id == data.GeneratedUserId || x.UserClaim.Any(a => a.ClaimValue == data.Department.Alias))
                        .Select(s => s.RealTimeConnection.LastOrDefault());

                  var users = users1.ToList(); //?
                  //.Where(w => w != null)
                  //check if user has customerRequest
                  if (entity.CustomerRequestId != null)
                  {
                      var newrequest = this.Uow.GetRepository<CustomerRequest>().GetById((int)entity.CustomerRequestId);

                      // Post data to the relevant users as realtime
                      RequestViewModel ViewModel = Mapper.Map<CustomerRequest, RequestViewModel>(newrequest);

                      SignalRHelper.Remoterequestupdate(ViewModel, users, _hubContext);

                      //post data to OrangeLine
                      HttpClientExtentions.PostAsBasicAuth(_configuration, entity.CustomerRequestId, (int)RequestStatusEnum.Icrada);

                  }
                  SignalRHelper.RemoteTaskadd(Mapper.Map<MainTask, TaskViewModel>(data), users, _hubContext);
              });


            output.Output = Mapper.Map<MainTask, TaskViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TaskCreateModel> entities)
        {
            var output = new LogicResult<ICollection<TaskViewModel>>();
            var data = Mapper.Map<List<TaskCreateModel>, List<MainTask>>(entities);
            foreach (var mainTask in data)
            {
                mainTask.TaskStatusId = (int)TaskStatusEnum.Yeni;
                mainTask.GeneratedUserId = GeneralUserId;
                mainTask.EndDate = DateTime.Now.AddDays(1);           //TODO Add default Day
            }
            var addedEntities = this.Uow.GetRepository<MainTask>().AddRange(data);
            output.Output = Mapper.Map<List<MainTask>, List<TaskViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Task Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TaskViewModel>>();
            var userDepartments = User.GetUserDepartments();
            var virtualDepartments = User.GetUserVirtualDepartments().Select(x => x.Alias.ToLower());
            var data = this.Uow.GetRepository<MainTask>().GetAll(OtherEntities);
            if (GeneralUserId != 0)
            {
                data = data.Where(x => x.ExecutorUserId == GeneralUserId
                || x.GeneratedUserId == GeneralUserId
                //|| x.ExecutorUser.ParentUserId == userId
                || x.GeneratedUser.ParentUserId == GeneralUserId
                //|| x.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                || userDepartments.Contains(x.Department.Alias)
                || virtualDepartments.Contains(x.ProjectName.ToLower()));
                //|| x.GeneratedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
            }
            output.TotalCount = data.Count();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            var returnedData = Mapper.Map<ICollection<MainTask>, ICollection<TaskViewModel>>(data.ToList());
            output.Output = returnedData.OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TaskViewModel>();
            var data = this.Uow.GetRepository<MainTask>().GetById(id, OtherEntities);
            output.Output = Mapper.Map<MainTask, TaskViewModel>(data);
            Result = output;

        }

        #endregion

        #region Task Find

        protected override void FindAbstract(TaskFindModel parameters)
        {
            var output = new LogicResult<TaskViewModel>();

            var predicate = PredicateBuilder.True<MainTask>();
            if (parameters == null)
            {
                parameters = new TaskFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.GeneratedUserId != null)
            {
                predicate = predicate.And(r => r.GeneratedUserId == parameters.GeneratedUserId);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.ExecutorUserId != null)
            {
                predicate = predicate.And(r => parameters.ExecutorUserId.Contains(r.ExecutorUserId));
            }
            if (parameters.InjuryTypeId != null)
            {
                predicate = predicate.And(r => r.InjuryTypeId == parameters.InjuryTypeId);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.StartDateTo);
            }
            if (parameters.EndDateFrom != null)
            {
                predicate = predicate.And(r => r.EndDate >= parameters.EndDateFrom);
            }
            if (parameters.EndDateTo != null)
            {
                predicate = predicate.And(r => r.EndDate <= parameters.EndDateTo);
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            if (parameters.TaskStatusId != null)
            {
                predicate = predicate.And(r => parameters.TaskStatusId.Contains((int)r.TaskStatusId));
            }
            if (parameters.ProjectId != null)
            {
                predicate = predicate.And(r => parameters.ProjectId.Contains(r.ProjectId));
            }
            if (parameters.ProjectName != null)
            {
                predicate = predicate.And(r => parameters.ProjectName.Contains(r.ProjectName));
            }
            if (parameters.RatingId != null)
            {
                predicate = predicate.And(r => r.RatingId == parameters.RatingId);
            }
            var data = this.Uow.GetRepository<MainTask>().Find(predicate, OtherEntities);
            output.Output = Mapper.Map<MainTask, TaskViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(TaskFindModel parameters)
        {

            if (parameters.AfterHour != null)
            {
                if (parameters.TaskStatusId != null && parameters.TaskStatusId.Count > 0)
                {
                    parameters.TaskStatusId.Clear();
                }
                else
                {
                    parameters.TaskStatusId = new List<int>();
                }
                parameters.TaskStatusId.Add(1);
            }


            var output = new LogicResult<ICollection<TaskViewModel>>();
            var predicate = PredicateBuilder.True<MainTask>();
            if (parameters == null)
            {
                parameters = new TaskFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.GeneratedUserId != null)
            {
                predicate = predicate.And(r => r.GeneratedUserId == parameters.GeneratedUserId);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.ExecutorUserId != null)
            {
                predicate = predicate.And(r => parameters.ExecutorUserId.Contains(r.ExecutorUserId));
            }
            if (parameters.InjuryTypeId != null)
            {
                predicate = predicate.And(r => r.InjuryTypeId == parameters.InjuryTypeId);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.StartDateTo);
            }
            if (parameters.EndDateFrom != null)
            {
                predicate = predicate.And(r => r.EndDate >= parameters.EndDateFrom);
            }
            if (parameters.EndDateTo != null)
            {
                predicate = predicate.And(r => r.EndDate <= parameters.EndDateTo);
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            if (parameters.TaskStatusId != null)
            {
                predicate = predicate.And(r => parameters.TaskStatusId.Contains((int)r.TaskStatusId));
            }
            if (parameters.ProjectId != null)
            {
                predicate = predicate.And(r => parameters.ProjectId.Contains(r.ProjectId));
            }
            if (parameters.ProjectName != null)
            {
                predicate = predicate.And(r => parameters.ProjectName.Contains(r.ProjectName));
            }
            if (parameters.RatingId != null)
            {
                predicate = predicate.And(r => r.RatingId == parameters.RatingId);
            }
            var userDepartments = User.GetUserDepartments();

            //foreach (var dep in userDepartments)
            //{
            //    Console.WriteLine(dep);
            //}
            //Console.WriteLine("------------------");

            var virtualDepartments = User.GetUserVirtualDepartments()?.Select(x => x.Alias.ToLower());

            //foreach (var dep in virtualDepartments)
            //{
            //    Console.WriteLine(dep);
            //}
            //  var data = this.Uow.GetRepository<MainTask>().FindAll(predicate, OtherEntities);

            var data = Uow.NisContext.MainTask.Where(w => w.Id > 0).AsQueryable();//.Count();


            if (GeneralUserId != 0)
            {
                data = data
                           .Include(ct => ct.GeneratedUser)
                           .ThenInclude(ctd => ctd.Department)
                           .Include(ss => ss.Department)
                           .Include(ts => ts.Project)
                           .Where(x => x.ExecutorUserId == GeneralUserId
                             || x.GeneratedUserId == GeneralUserId
                             || x.GeneratedUser.ParentUserId == GeneralUserId
                              //|| x.ChildTasks.Any(a => a.ExecutorUserId == userId)
                              //|| x.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                              || userDepartments.Contains(x.Department.Alias)
                              || virtualDepartments.Contains(x.ProjectName.ToLower())).AsQueryable();
                //|| x.GeneratedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
            }
            data = data.Where(predicate);
            data = data.Where(a => a.ParentTask == null || a.ExecutorUserId == GeneralUserId);

            if (parameters.AfterHour != null && parameters.AfterHour.Count > 0)
            {
                var afterHour = parameters.AfterHour.Min();
                data = data.Where(w => (DateTime.Now - (DateTime)w.CreatedDate).TotalHours >= afterHour);
            }



            output.TotalCount = data.Count();
       
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }

            foreach (var _include in OtherEntities)
            {
                data.Include(_include).Load();
            }



            var at = data;
            var returnedData = Mapper.Map<ICollection<MainTask>, ICollection<TaskViewModel>>(data.ToList());
            if (parameters.Id != null)
            {
                if (returnedData.All(x => x.Id != parameters.Id))
                {
                    returnedData.Add(Mapper.Map<MainTask, TaskViewModel>(at.FirstOrDefault(a => a.Id == parameters.Id)));
                }

            }
            output.Output = returnedData.OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region Task Update

        protected override void UpdateAbstract(TaskUpdateModel entity)
        {
            var output = new LogicResult<TaskViewModel>();
            var data = Uow.NisContext.MainTask.AsQueryable();//.Count();
            data = data.Where(w => w.Id == entity.Id);
            //"ExecutorUser", "InjuryType", "RealInjuryType", "TaskStatus", "Tag", "Rating", "ParentTask", "ChildTasks"
            var curentMainTask = data.Include(i => i.CustomerRequest)
                  .Include(i => i.Attachment)
                  .Include(i => i.ExecutorUser)
                  .Include(i => i.InjuryType)
                  .Include(i => i.RealInjuryType)
                  .Include(i => i.TaskStatus)
                  .Include(i => i.Tag)
                  .Include(i => i.Rating)
                  .Include(i => i.ParentTask)
                  .Include(i => i.ChildTasks)
                  .FirstOrDefault();

            //var curentMainTask = this.Uow.GetRepository<MainTask>().GetById(entity.Id);
            //foreach (var other in OtherEntitiesUpdate)
            //{
            //    data.Include(other).Load();
            //}
            var mainTask = Mapper.Map(entity, curentMainTask);
            for (int i = 0; i < entity.Tag.Count; i++)
            {
                mainTask.Tag.ToList()[i].MainTaskId = curentMainTask.Id;
            }
            mainTask.UpdatedUserId = GeneralUserId;
            var dataResult = this.Uow.GetRepository<MainTask>().Update(mainTask);
            output.Output = Mapper.Map<MainTask, TaskViewModel>(dataResult);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TaskUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Task Remove

        protected override void RemoveAbstract(TaskDeleteModel entity)
        {
            var output = new LogicResult<TaskViewModel>();
            var data = Uow.GetRepository<MainTask>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<MainTask>().Remove(data);
            }
            else
            {
                output.ErrorList.Add(new Error
                {
                    Code = "400",
                    Text = "id is incorrect",
                    Type = OperationResultCode.Information
                });
            }
            Result = output;
        }

        #endregion
    }
}
