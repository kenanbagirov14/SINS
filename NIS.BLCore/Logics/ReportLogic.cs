using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using NIS.BLCore.Extensions;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.Report.Request.CompletedRequest;
using NIS.BLCore.Models.Report.Request.Department;
using NIS.BLCore.Models.Report.Request.Region;
using NIS.BLCore.Models.Report.Request.RequestStatus;
using NIS.BLCore.Models.Report.Request.RequestType;
using NIS.BLCore.Models.Report.Task.Department;
using NIS.BLCore.Models.Report.Task.ExecutorUser;
using NIS.BLCore.Models.Report.Task.TaskStatus;
using NIS.DALCore.Context;
using NIS.DALCore.UnitOfWork;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class ReportLogic
    {

        #region Properties

        public NISContext NisContext;

        public UnitOfWork Uow { get; set; }
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        public string[] RequestEntities = new[] { "MainTask", "CreatedUser", "Region", "RequestStatus", "SourceType", "CustomerRequestType", "Department", "MainTask.TaskStatus" };
        public string[] TaskEntities = new[] { "CustomerRequest", "Attachment", "Department", "ExecutorUser", "GeneratedUser", "InjuryType", "TaskStatus", "Project", "Tag", "Rating", "ParentTask", "ChildTasks" };
        private ClaimsPrincipal _User;

        public ReportLogic(ClaimsPrincipal User)
        {
            _User = User;
            Uow = new UnitOfWork();
        }


        #endregion

        #region Request

        public RequestDepartmentReport RequestByDepartment(RequestFindModel parameters)
        {
            var predicate = PredicateBuilder.True<CustomerRequest>();
            if (parameters == null)
            {
                parameters = new RequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestTypeId != null)
            {
                predicate = predicate.And(r => parameters.CustomerRequestTypeId.Contains(r.CustomerRequestTypeId));
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.CreatedUserId != null)
            {
                predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
            }
            if (parameters.CustomerName != null)
            {
                predicate = predicate.And(r => r.CustomerName == parameters.CustomerName);
            }
            if (parameters.CustomerNumber != null)
            {
                predicate = predicate.And(r => r.CustomerNumber == parameters.CustomerNumber);
            }
            if (parameters.Text != null)
            {
                predicate = predicate.And(r => r.Text.Contains(parameters.Text));
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.StartDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.StartDate <= parameters.StartDateTo);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.ContactNumber != null)
            {
                predicate = predicate.And(r => r.ContactNumber == parameters.ContactNumber);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => parameters.RequestStatusId.Contains(r.RequestStatusId));
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
            }
            var data = this.Uow.GetRepository<CustomerRequest>().FindAll(predicate, RequestEntities);

            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            if (userId != 0)
            {
                data = data.Where(x => x.CreatedUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.CustomerRequestType.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                Departmentid = x.DepartmentId,
                Departmentname = x.Department.Name
            });

            RequestDepartmentReport departmentReport = new RequestDepartmentReport();

            foreach (var aa in groupedData)
            {
                if (aa.Key.Departmentid != null)
                {
                    departmentReport.Data.Add(new RequestByDepartmentViewModel
                    {
                        Id = aa.Key.Departmentid,
                        Name = aa.Key.Departmentname,
                        Value = data.Count(x => x.DepartmentId == aa.Key.Departmentid)
                    });
                }
                else
                {
                    //add code
                    departmentReport.Data.Add(new RequestByDepartmentViewModel
                    {
                        Id = aa.Key.Departmentid,
                        Name = "Abonent Müraciəti",
                        Value = data.Count(x => x.DepartmentId == aa.Key.Departmentid)
                    });
                    //////////
                }

            }
            departmentReport.TotalCount = data.Count();
            departmentReport.Data = departmentReport.Data.OrderByDescending(x => x.Value).ToList();
            return departmentReport;
        }


        public RequestRegionReport RequestByRegion(RequestFindModel parameters)
        {
            var predicate = PredicateBuilder.True<CustomerRequest>();
            if (parameters == null)
            {
                parameters = new RequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestTypeId != null)
            {
                predicate = predicate.And(r => parameters.CustomerRequestTypeId.Contains(r.CustomerRequestTypeId));
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.CreatedUserId != null)
            {
                predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
            }
            if (parameters.CustomerName != null)
            {
                predicate = predicate.And(r => r.CustomerName == parameters.CustomerName);
            }
            if (parameters.CustomerNumber != null)
            {
                predicate = predicate.And(r => r.CustomerNumber == parameters.CustomerNumber);
            }
            if (parameters.Text != null)
            {
                predicate = predicate.And(r => r.Text.Contains(parameters.Text));
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.StartDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.StartDate <= parameters.StartDateTo);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.ContactNumber != null)
            {
                predicate = predicate.And(r => r.ContactNumber == parameters.ContactNumber);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => parameters.RequestStatusId.Contains(r.RequestStatusId));
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
            }

            var data = this.Uow.GetRepository<CustomerRequest>().FindAll(predicate, RequestEntities);
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            if (userId != 0)
            {
                data = data.Where(x => x.CreatedUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.CustomerRequestType.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                Region = x.Region,
            });

            RequestRegionReport departmentReport = new RequestRegionReport();

            foreach (var aa in groupedData)
            {
                var regionid = aa.Key.Region?.Id;
                departmentReport.Data.Add(new RequestByRegionViewModel
                {
                    Id = regionid,
                    Name = aa.Key.Region?.Name,
                    Value = data.Count(x => x.DepartmentId == regionid),
                    CopmletedValue = data.Count(x => x.RequestStatus.Id == 3 && x.Region.Id == regionid),
                    Rating = data.Any(x => x.Region.Id == regionid && x.RatingId != null) ? (data.Where(x => x.Region.Id == regionid).Sum(a => a.Rating.TaskPoint) / data.Count(x => x.Region.Id == regionid && x.RatingId != null)) : 0
                });
            }
            departmentReport.TotalCount = data.Count();
            departmentReport.Data = departmentReport.Data.OrderByDescending(x => x.Value).ToList();
            return departmentReport;
        }


        public RequestRequestTypeReport RequestByRequestType(RequestFindModel parameters)
        {
            var predicate = PredicateBuilder.True<CustomerRequest>();
            if (parameters == null)
            {
                parameters = new RequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestTypeId != null)
            {
                predicate = predicate.And(r => parameters.CustomerRequestTypeId.Contains(r.CustomerRequestTypeId));
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.CreatedUserId != null)
            {
                predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
            }
            if (parameters.CustomerName != null)
            {
                predicate = predicate.And(r => r.CustomerName == parameters.CustomerName);
            }
            if (parameters.CustomerNumber != null)
            {
                predicate = predicate.And(r => r.CustomerNumber == parameters.CustomerNumber);
            }
            if (parameters.Text != null)
            {
                predicate = predicate.And(r => r.Text.Contains(parameters.Text));
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.StartDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.StartDate <= parameters.StartDateTo);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.ContactNumber != null)
            {
                predicate = predicate.And(r => r.ContactNumber == parameters.ContactNumber);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => parameters.RequestStatusId.Contains(r.RequestStatusId));
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
            }

            var data = this.Uow.GetRepository<CustomerRequest>().FindAll(predicate, RequestEntities);
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            if (userId != 0)
            {
                data = data.Where(x => x.CreatedUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.CustomerRequestType.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                RequestTypeid = x.CustomerRequestTypeId,
                RequestTypename = x.CustomerRequestType.Name
            });

            RequestRequestTypeReport requestTypeReport = new RequestRequestTypeReport();

            foreach (var aa in groupedData)
            {
                requestTypeReport.Data.Add(new RequestByRequestTypeViewModel
                {
                    Id = aa.Key.RequestTypeid,
                    Name = aa.Key.RequestTypename,
                    Value = data.Count(x => x.CustomerRequestTypeId == aa.Key.RequestTypeid)
                });
            }
            requestTypeReport.TotalCount = data.Count();
            requestTypeReport.Data = requestTypeReport.Data.OrderByDescending(x => x.Value).ToList();
            return requestTypeReport;
        }

        public RequestRequestStatusReport RequestByRequestStatus(RequestFindModel parameters)
        {
            var predicate = PredicateBuilder.True<CustomerRequest>();
            if (parameters == null)
            {
                parameters = new RequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestTypeId != null)
            {
                predicate = predicate.And(r => parameters.CustomerRequestTypeId.Contains(r.CustomerRequestTypeId));
            }
            if (parameters.DepartmentId != null)
            {
                predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
            }
            if (parameters.CreatedUserId != null)
            {
                predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
            }
            if (parameters.CustomerName != null)
            {
                predicate = predicate.And(r => r.CustomerName == parameters.CustomerName);
            }
            if (parameters.CustomerNumber != null)
            {
                predicate = predicate.And(r => r.CustomerNumber == parameters.CustomerNumber);
            }
            if (parameters.Text != null)
            {
                predicate = predicate.And(r => r.Text.Contains(parameters.Text));
            }
            if (parameters.StartDateFrom != null)
            {
                predicate = predicate.And(r => r.StartDate >= parameters.StartDateFrom);
            }
            if (parameters.StartDateTo != null)
            {
                predicate = predicate.And(r => r.StartDate <= parameters.StartDateTo);
            }
            if (parameters.CreatedDateFrom != null)
            {
                predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
            }
            if (parameters.CreatedDateTo != null)
            {
                predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.ContactNumber != null)
            {
                predicate = predicate.And(r => r.ContactNumber == parameters.ContactNumber);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            if (parameters.RequestStatusId != null)
            {
                predicate = predicate.And(r => parameters.RequestStatusId.Contains(r.RequestStatusId));
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
            }

            var data = this.Uow.GetRepository<CustomerRequest>().FindAll(predicate, RequestEntities);
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            if (userId != 0)
            {
                data = data.Where(x => x.CreatedUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.CustomerRequestType.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                RequestStatusid = x.RequestStatusId,
                RequestStatusname = x.RequestStatus.Name
            });

            RequestRequestStatusReport requestStatusReport = new RequestRequestStatusReport();

            foreach (var aa in groupedData)
            {
                requestStatusReport.Data.Add(new RequestByRequestStatusViewModel
                {
                    Id = aa.Key.RequestStatusid,
                    Name = aa.Key.RequestStatusname,
                    Value = data.Count(x => x.RequestStatusId == aa.Key.RequestStatusid)
                });
            }
            requestStatusReport.TotalCount = data.Count();
            requestStatusReport.Data = requestStatusReport.Data.OrderByDescending(x => x.Value).ToList();
            return requestStatusReport;
        }


        /// <summary>
        /// ///////

        //    #region completedRequest
        //    public RequstByCompletedReport RequestByCompletedRequest(RequestFindModel parameters)
        //    {
        //        parameters.RequestStatusId = new List<int?>();

        //        // var a = parameters.RequestStatusId;
        //        //  parameters.RequestStatusId.Add(5);
        //        //    parameters.RequestStatusId.Add(5);
        //        var predicate = PredicateBuilder.True<CustomerRequestArchive>();
        //        if (parameters == null)
        //        {
        //            parameters = new RequestFindModel();
        //        }
        //        if (parameters.Id != null)
        //        {
        //            predicate = predicate.And(r => r.Id == parameters.Id);
        //        }
        //        if (parameters.CustomerRequestTypeId != null)
        //        {
        //            predicate = predicate.And(r => parameters.CustomerRequestTypeId.Contains(r.CustomerRequestTypeId));
        //        }
        //        if (parameters.DepartmentId != null)
        //        {
        //            predicate = predicate.And(r => parameters.DepartmentId.Contains(r.DepartmentId));
        //        }
        //        if (parameters.CreatedUserId != null)
        //        {
        //            predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
        //        }
        //        if (parameters.CustomerName != null)
        //        {
        //            predicate = predicate.And(r => r.CustomerName == parameters.CustomerName);
        //        }
        //        if (parameters.CustomerNumber != null)
        //        {
        //            predicate = predicate.And(r => r.CustomerNumber == parameters.CustomerNumber);
        //        }
        //        if (parameters.Text != null)
        //        {
        //            predicate = predicate.And(r => r.Text.Contains(parameters.Text));
        //        }
        //        if (parameters.StartDateFrom != null)
        //        {
        //            predicate = predicate.And(r => r.StartDate >= parameters.StartDateFrom);
        //        }
        //        if (parameters.StartDateTo != null)
        //        {
        //            predicate = predicate.And(r => r.StartDate <= parameters.StartDateTo);
        //        }
        //        if (parameters.CreatedDateFrom != null)
        //        {
        //            predicate = predicate.And(r => r.CreatedDate >= parameters.CreatedDateFrom);
        //        }
        //        if (parameters.CreatedDateTo != null)
        //        {
        //            predicate = predicate.And(r => r.CreatedDate <= parameters.CreatedDateTo);
        //        }
        //        if (parameters.Description != null)
        //        {
        //            predicate = predicate.And(r => r.Description == parameters.Description);
        //        }
        //        if (parameters.ContactNumber != null)
        //        {
        //            predicate = predicate.And(r => r.ContactNumber == parameters.ContactNumber);
        //        }
        //        if (parameters.SourceTypeId != null)
        //        {
        //            predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
        //        }
        //        if (parameters.RequestStatusId != null)
        //        {
        //            predicate = predicate.And(r => parameters.RequestStatusId.Contains(r.RequestStatusId));
        //        }
        //        if (parameters.RegionId != null)
        //        {
        //            predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
        //        }

        //         var data1 = this.Uow.GetRepository<CustomerRequestArchive>().GetAll();

        //        //var data = Mapper.Map<IQueryable<CustomerRequestArchive>, List<CustomerRequest>>(returnedData);

        //        var userId = _User.GetUserId();
        //        var UserDepartments = _User.GetUserDepartments();


        //        List<CustomerRequestArchive> data2 = new List<CustomerRequestArchive>();
        //        IEnumerable<CustomerRequestArchive> data = new List<CustomerRequestArchive>();

        //           foreach(var i in data1)
        //          {
        //            i.CustomerRequestType = this.Uow.NisContext.CustomerRequestType.FirstOrDefault(f => f.Id == i.CustomerRequestTypeId);
        //            i.CustomerRequestType.Department = this.Uow.NisContext.Department.FirstOrDefault(f => f.Id == i.CustomerRequestTypeId);

        //            data2.Add(i);
        //          }

        //        if (userId != 0)
        //        {
        //            data = data2.Where(x => x.CreatedUserId == userId

        //            || x.CustomerRequestType.Department.DefaultUserId == userId

        //            || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
        //        }



        //        var groupedData = data.GroupBy(x => new
        //        {
        //            RequestStatusid = x.RequestStatusId,
        //            RequestStatusname = x.RequestStatus.Name,
        //            Request = Mapper.Map<CustomerRequestArchive, RequestViewModel>(x)
        //    });

        //        RequstByCompletedReport requestStatusReport = new RequstByCompletedReport();

        //        foreach (var aa in groupedData)
        //        {

        //            requestStatusReport.Data.Add(new RquestCompletedRespons
        //            {

        //                Id = aa.Key.RequestStatusid,
        //                Name = aa.Key.RequestStatusname,
        //                RequestView = aa.Key.Request,
        //                Value = data.Count(x => x.RequestStatusId == aa.Key.RequestStatusid)
        //            });

        //            var asas = aa;
        //        }
        //         requestStatusReport.TotalCount = data.Count();
        //         requestStatusReport.Data = requestStatusReport.Data.OrderByDescending(x => x.Value).ToList();
        //         return requestStatusReport;

        //    }
        //#endregion

        //// 

        #endregion

        #region Task

        public TaskTaskStatusReport TaskByTaskStatus(TaskFindModel parameters)
        {
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
            if (parameters.RatingId != null)
            {
                predicate = predicate.And(r => r.RatingId == parameters.RatingId);
            }
            var data = this.Uow.GetRepository<MainTask>().FindAll(predicate, TaskEntities);
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            var at = data;
            if (userId != 0)
            {
                data = data.Where(x => x.ExecutorUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                TaskStatusid = x.TaskStatusId,
                TaskStatusname = x.TaskStatus.Name
            });

            TaskTaskStatusReport taskStatusReport = new TaskTaskStatusReport();

            foreach (var aa in groupedData)
            {
                taskStatusReport.Data.Add(new TaskByTaskStatusViewModel
                {
                    Id = aa.Key.TaskStatusid,
                    Name = aa.Key.TaskStatusname,
                    Value = data.Count(x => x.TaskStatusId == aa.Key.TaskStatusid)
                });
            }
            taskStatusReport.TotalCount = data.Count();
            taskStatusReport.Data = taskStatusReport.Data.OrderByDescending(x => x.Value).ToList();
            return taskStatusReport;
        }

        public TaskExecutorUserReport TaskByExecutorUser(TaskFindModel parameters)
        {
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
            if (parameters.RatingId != null)
            {
                predicate = predicate.And(r => r.RatingId == parameters.RatingId);
            }
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            var data = this.Uow.GetRepository<MainTask>().FindAll(predicate, TaskEntities);

            var at = data;
            if (userId != 0)
            {
                data = data.Where(x => x.ExecutorUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                ExecutorUser = x.ExecutorUser

            });

            TaskExecutorUserReport executorUserReport = new TaskExecutorUserReport();

            foreach (var aa in groupedData)
            {
                var executerid = aa.Key.ExecutorUser?.Id;

                executorUserReport.Data.Add(new TaskByExecutorUserViewModel
                {
                    Id = aa.Key.ExecutorUser?.Id,
                    Name = aa.Key.ExecutorUser?.UserName,
                    Value = data.Count(x => x.ExecutorUser.Id == executerid),
                    CompletedValue = data.Count(x => x.TaskStatus.Id == 3 && x.ExecutorUser.Id == executerid),
                    Rating = data.Any(x => x.ExecutorUser.Id == executerid && x.RatingId != null) ? (data.Where(x => x.ExecutorUser.Id == executerid).Sum(a => a.Rating.TaskPoint) / data.Count(x => x.ExecutorUser.Id == executerid && x.RatingId != null)) : 0
                });
            }
            executorUserReport.TotalCount = data.Count();
            executorUserReport.Data = executorUserReport.Data.OrderByDescending(x => x.Value).ToList();
            executorUserReport.TotalData = new TaskByExecutorUserViewModel
            {
                Name = "Cəm",
                Value = executorUserReport.TotalCount,
                CompletedValue = executorUserReport.Data.Sum(x => x.CompletedValue),
                Rating = executorUserReport.Data.Sum(x => x.Rating) / executorUserReport.Data.Count()
            };
            return executorUserReport;
        }


        public TaskDepartmentReport TaskByDepartment(TaskFindModel parameters)
        {
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
            if (parameters.RatingId != null)
            {
                predicate = predicate.And(r => r.RatingId == parameters.RatingId);
            }
            var userId = _User.GetUserId();
            var UserDepartments = _User.GetUserDepartments();
            var data = this.Uow.GetRepository<MainTask>().FindAll(predicate, TaskEntities);

            var at = data;
            if (userId != 0)
            {
                data = data.Where(x => x.ExecutorUserId == userId
                //|| x.CreatedUser.ParentUserId == userId
                //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                || x.Department.DefaultUserId == userId
                || UserDepartments.Contains(x.Department.Alias));
            }
            var groupedData = data.GroupBy(x => new
            {
                Department = x.Department

            });

            TaskDepartmentReport executorUserReport = new TaskDepartmentReport();

            foreach (var aa in groupedData)
            {
                var departmentid = aa.Key.Department?.Id;

                executorUserReport.Data.Add(new TaskByDepartmentViewModel
                {
                    Id = aa.Key.Department?.Id,
                    Name = aa.Key.Department?.Name,
                    Value = data.Count(x => x.Department.Id == departmentid),
                    CompletedValue = data.Count(x => x.TaskStatus.Id == 3 && x.Department.Id == departmentid),
                    Rating = data.Any(x => x.Department.Id == departmentid && x.RatingId != null) ? (data.Where(x => x.Department.Id == departmentid).Sum(a => a.Rating.TaskPoint) / data.Count(x => x.Department.Id == departmentid && x.RatingId != null)) : 0
                });
            }
            executorUserReport.TotalCount = data.Count();
            executorUserReport.Data = executorUserReport.Data.OrderByDescending(x => x.Value).ToList();
            executorUserReport.TotalData = new TaskByDepartmentViewModel
            {
                Name = "Cəm",
                Value = executorUserReport.TotalCount,
                CompletedValue = executorUserReport.Data.Sum(x => x.CompletedValue),
                Rating = executorUserReport.Data.Sum(x => x.Rating) / executorUserReport.Data.Count()
            };
            return executorUserReport;
        }


        #endregion
    }
}
