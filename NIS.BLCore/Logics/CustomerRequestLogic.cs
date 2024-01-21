using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Base;
using NIS.BLCore.DTO;
using NIS.BLCore.Extensions;
using NIS.BLCore.Helpers;
using NIS.BLCore.Hubs;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.Notification;
using NIS.BLCore.Models.SignalR;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;
using Task = System.Threading.Tasks.Task;

namespace NIS.BLCore.Logics
{
    public class CustomerRequestLogic : BaseLogic<RequestViewModel, RequestCreateModel, RequestFindModel, RequestUpdateModel, RequestDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private ClaimsPrincipal _user;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        private readonly IHubContext<NisHub> _hubContext;
        private IConfiguration _configuration;
        public string[] OtherEntities = new[] { "Attachment", "MainTask", "CreatedUser", "Region", "RequestStatus", "RequestEmail", "RequestStatusHistory", "SourceType", "MainTask.TaskStatus", "Rating" };
        public string[] OtherEntitiesForAdd = new[] { "CreatedUser", "Department", "CreatedUser.Department" };
        #endregion

        #region Constructor

        public CustomerRequestLogic(ClaimsPrincipal User, IHubContext<NisHub> hubContext, IConfiguration configuration) : base(User)
        {
            _hubContext = hubContext;
            _user = User;
            _configuration = configuration;
        }

        #endregion


        #region Request Add

        protected override void AddAbstract(RequestCreateModel entity)
        {

            //Changes Codes
            string test = $"{entity.CustomerRequestTypeId}_{entity.CustomerNumber}";

            if (entity.Description != null && entity.CustomerNumber == null)
            {
                if (entity.MainTask == null && entity.MainTask.Count >= 1)
                {
                    test = $"{entity.Description}";

                }
                else
                {
                    test = $"{entity.MainTask.FirstOrDefault().Description}_{entity.MainTask.FirstOrDefault().ExecutorUserId}";
                }
            }
            //Changes Codes


            var output = new LogicResult<RequestViewModel>();

            if (DataHolder.DailyInsertedPhones(test) )
            {
                output.Output = Mapper.Map<RequestCreateModel, RequestViewModel>(entity);
                output.ErrorList.Add(new Error() { Code = "407", Type = OperationResultCode.Error, Text = "Bu Tapşırıq Artıq Mövcuddur Yenisi üçün 4 saat gözləməlisiniz" });
                Result = output;
                return;
            }
//////////////Add Code////////////
            if (entity.SourceTypeId==2)
            {
                var request = this.Uow.NisContext.CustomerRequest.FirstOrDefault(f => f.CustomerRequestTypeId == entity.CustomerRequestTypeId && f.CustomerNumber == entity.CustomerNumber && f.RequestStatusId == 2 && f.SourceTypeId == 2);
                if (request != null)
                {
                    output.Output = Mapper.Map<RequestCreateModel, RequestViewModel>(entity);
                    output.ErrorList.Add(new Error() { Code = "408", Type = OperationResultCode.Error, Text = entity.CustomerNumber + "-nömrəsindən eyni başlıqlı sorğu mövcuddur ve hal hazırda icradadır" });
                    Result = output;
                    return;
                }
            }
///////////////////////////////////  

            var data = Mapper.Map<RequestCreateModel, CustomerRequest>(entity);
            data.RequestStatusId = (int)RequestStatusEnum.Yeni;
            data.CreatedUserId = GeneralUserId;            //Get UserId from HTTP Context
            data.CreatedDate = DateTime.Now;
            if (data.MainTask.Count > 0)
            {
                foreach (var item in data.MainTask)
                {
                    item.GeneratedUserId = GeneralUserId;
                    item.CreatedDate = DateTime.Now;
                    item.TaskStatusId = 1;
                }
            }
            if (entity.SourceTypeId == (int)SourceTypeEnum.Abonent)
            {
                var prefix = entity.CustomerNumber.ToString().Substring(0, 2);
                if (prefix != "12" && prefix != "18")
                {
                    prefix = entity.CustomerNumber.ToString().Substring(0, 4);
                }
                int iprefix = int.Parse(prefix);
                var region = Uow.GetRepository<Region>().GetAll().FirstOrDefault(d => d.RegionPrefix == iprefix);
                data.RegionId = region?.Id;
            }
            this.Uow.GetRepository<CustomerRequest>().Add(data); //, OtherEntitiesForAdd
            int mm = this.Uow.SaveChanges();

            ///////change code
            if (mm > 0)
            {
                var _CreteUser = this.Uow.NisContext.User.FirstOrDefault(f => f.Id == data.CreatedUserId);
                var _CreteUserDep = this.Uow.NisContext.Department.FirstOrDefault(f => f.Id == _CreteUser.DepartmentId);
                if (entity.SourceTypeId == (int)SourceTypeEnum.Daxili)
                {
                    var _Dep = this.Uow.NisContext.Department.FirstOrDefault(f => f.Id == data.DepartmentId);
                    var _DepCustumerReq = this.Uow.NisContext.CustomerRequest.Where(r => r.DepartmentId == data.DepartmentId).ToList();
                    data.Department = _Dep;
                }
                else
                {
                    var _RegionCustumerReq = this.Uow.NisContext.CustomerRequest.Where(r => r.RegionId == data.RegionId).ToList();
                    data.Region.CustomerRequest = _RegionCustumerReq;
                }
                data.CreatedUser = _CreteUser;
                data.CreatedUser.Department = _CreteUserDep;
            }
            ///////change code
            ///
            if (data == null)
            {
                output.ErrorList.Add(new Error
                {
                    Code = "Adding exception",
                    Text = "Request add Failed",
                    Type = OperationResultCode.Error
                });
                return;
            }
            else
            {
                if (mm > 0 && entity.SourceTypeId != 3 && entity.DepartmentId != 2474)
                {
                    DataHolder.DailyInsertedPhones(test, true);
                }
            }
            var requestHistory = this.Uow.GetRepository<RequestStatusHistory>().Add(new RequestStatusHistory //TODO move this function to the trigger
            {
                UpdatedUserId = GeneralUserId,
                CustomerRequestId = data.Id,
                RequestStatusId = (int)RequestStatusEnum.Yeni,
                UpdatedDate = DateTime.Now
            });
            //return data to the controller
            data.RequestStatusHistory.Add(requestHistory);
            var returnedData = Mapper.Map<CustomerRequest, RequestViewModel>(data);
            var predicate = PredicateBuilder.True<User>();
            predicate = predicate.And(r => r.Id == data.CreatedUserId);
            predicate = predicate.Or(x => x.UserClaim.Any(a => a.ClaimValue == data.CreatedUser.Department.Alias));
            var users = this.Uow.GetRepository<User>().GetAll().Where(predicate).Where(a => a.RealTimeConnection.FirstOrDefault() != null).Select(s => s.RealTimeConnection.LastOrDefault()).ToList();
            returnedData.RequestStatus = new RequestStatusDto { Id = requestHistory.RequestStatusId, CreatedDate = DateTime.Now };
            ////send customerrequest data
            Task.Run(() =>
            {
                //  Find relevant users for signalR
                SignalRHelper.Remoterequestadd(returnedData, users, _hubContext);
            });
            output.Output = returnedData;
            Task.Run(() =>
            {
                if (entity.MainTask.Count > 0)
                {
                    foreach (var _output in returnedData.MainTask)
                    {
                        SignalrReturneddata<TaskViewModel> taskData = new SignalrReturneddata<TaskViewModel>()
                        {
                            NewData = Mapping<TaskViewModel, TaskDto>.MapToModel(_output)
                        };
                        var signalrTaskAddeddata = JObject.Parse(JsonConvert.SerializeObject(taskData,
                         new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                        _hubContext.Clients.All.SendAsync("remoteTaskadd", signalrTaskAddeddata);
                    }
                }
           });
            CustomerRequestTypeLogic _customerRequestTypeLogic = new CustomerRequestTypeLogic(_user);
            MainTaskLogic _mainTaskLogic = new MainTaskLogic(_configuration, _user, _hubContext);
            var customerRequestType = _customerRequestTypeLogic.GetById(returnedData.CustomerRequestTypeId);
            //TODO
            if (customerRequestType.Output.AutoCreateTask == true)
            {
                var dep = customerRequestType.Output.Department;
                var mainTask = _mainTaskLogic.Add(new TaskCreateModel()
                {
                    StartDate = DateTime.Now.ToLocalTime(),
                    EndDate = DateTime.Now.AddDays(14),
                    CustomerRequestId = returnedData.Id,
                    Description = $"{dep.Name}/{customerRequestType.Output.Name}",
                    Note = returnedData.Text,
                    DepartmentId = dep.Id,
                    GeneratedUserId = dep.CuratorUserId,
                    MainTaskId = null,
                    ExecutorUserId = dep.DefaultUserId,
                    Priority = 1,
                    Attachment = entity.Attachment
                });
            }
            Result = output;
        }


        protected override void AddRangeAbstract(List<RequestCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RequestViewModel>>();
            var data = Mapper.Map<List<RequestCreateModel>, List<CustomerRequest>>(entities);
            foreach (var customerRequest in data)
            {
                customerRequest.RequestStatusId = (int)RequestStatusEnum.Yeni;
                customerRequest.CreatedDate = DateTime.Now;
                customerRequest.CreatedUserId = GeneralUserId;
            }
            var addedEntities = this.Uow.GetRepository<CustomerRequest>().AddRange(data);
            output.Output = Mapper.Map<List<CustomerRequest>, List<RequestViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Request Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RequestViewModel>>();
            var UserDepartments = User.GetUserDepartments();
            var data = this.Uow.GetRepository<CustomerRequest>().GetAll(OtherEntities);
            if (GeneralUserId != 0)
            {
                data = data.Where(x => x.CustomerRequestType.Department.DefaultUserId == GeneralUserId
                 || x.CreatedUserId == GeneralUserId
                 //|| x.CreatedUser.ParentUserId == GeneralUserId
                 //|| x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == GeneralUserId) != null
                 //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == GeneralUserId) != null);
                 || UserDepartments.Contains(x.CustomerRequestType.Department.Alias));
            }
            output.TotalCount = data.Count();

            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }

            var listdata = data.ToList();
            var returnedData = Mapper.Map<ICollection<CustomerRequest>, List<RequestViewModel>>(listdata);

            for (int i = 0; i < returnedData.Count; i++)
            {
                returnedData[i].RequestStatus.CreatedDate = listdata[i].RequestStatusHistory.LastOrDefault(x => x.RequestStatusId == listdata[i].RequestStatusId)?.UpdatedDate;
            }
            output.Output = returnedData.OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RequestViewModel>();
            var data = this.Uow.GetRepository<CustomerRequest>().GetById(id, OtherEntities);
            var returnedData = Mapper.Map<CustomerRequest, RequestViewModel>(data);
            var requestStatusHistory = data?.RequestStatusHistory?.OrderByDescending(x => x.Id)
                    .FirstOrDefault();
            if (requestStatusHistory != null)
            {
                returnedData.RequestStatus = Mapper.Map<RequestStatus, RequestStatusDto>(requestStatusHistory.RequestStatus);
            }
            output.Output = returnedData;
            Result = output;
        }

        #endregion

        #region Request Find

        protected override void FindAbstract(RequestFindModel parameters)
        {
            var output = new LogicResult<RequestViewModel>();
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
            if (parameters.AON != null)
            {
                predicate = predicate.And(r => parameters.AON.Contains(r.AON));
            }
            if (parameters.ContractNumber != null)
            {
                predicate = predicate.And(r => parameters.ContractNumber.Contains(r.ContractNumber));
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
            var data = this.Uow.GetRepository<CustomerRequest>().Find(predicate, OtherEntities);
            var returnedData = Mapper.Map<CustomerRequest, RequestViewModel>(data);

            var requestStatusHistory = data?.RequestStatusHistory?.OrderByDescending(x => x.Id)
                    .FirstOrDefault();
            if (requestStatusHistory != null)
            {
                returnedData.RequestStatus = Mapper.Map<RequestStatus, RequestStatusDto>(requestStatusHistory.RequestStatus);
            }
            output.Output = returnedData;
            Result = output;
        }

        protected override void FindAllAbstract(RequestFindModel parameters)
        {
            var output = new LogicResult<ICollection<RequestViewModel>>();
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
            if (parameters.AON != null)
            {
                predicate = predicate.And(r => parameters.AON.Contains(r.AON));
            }
            if (parameters.ContractNumber != null)
            {
                predicate = predicate.And(r => parameters.ContractNumber.Contains(r.ContractNumber));
            }
            if (parameters.CreatedUserId != null)
            {
                predicate = predicate.And(r => r.CreatedUserId == parameters.CreatedUserId);
            }
            if (parameters.CustomerName != null)
            {
                predicate = predicate.And(r => r.CustomerName.Contains(parameters.CustomerName));
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
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
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
            if (parameters.RequestStatusId == null)
            {
                predicate = predicate.And(r => r.RequestStatusId == (int)RequestStatusEnum.Yeni || r.RequestStatusId == (int)RequestStatusEnum.Icrada || r.RequestStatusId == (int)RequestStatusEnum.IcraOlundu);
            }
            if (parameters.RegionId != null)
            {
                predicate = predicate.And(r => parameters.RegionId.Contains(r.RegionId));
            }
            var userId = GeneralUserId;
            var UserDepartments = User.GetUserDepartments();
            var inek = UserDepartments.ToArray();
            //var data = this.Uow.GetRepository<CustomerRequest>().FindAll(predicate, OtherEntities);
            var data = Uow.NisContext.CustomerRequest.AsQueryable();//.Count();

            if (userId != 0)
            {
                data = data
                     .Include(ct => ct.CustomerRequestType)
                     .Include(ctd => ctd.Department)
                     .Where(x => x.CustomerRequestType.Department.DefaultUserId == userId
                                     || x.CreatedUserId == userId
                                     || x.CustomerRequestType.Department.CuratorUserId == userId
                                     //|| x.CreatedUser.ParentUserId == userId
                                     // || x.CreatedUser.Department.Users.FirstOrDefault(a => a.Id == userId) != null
                                     //|| x.CustomerRequestType.Department.Users.FirstOrDefault(a => a.Id == userId) != null);
                                     || UserDepartments.Contains(x.CustomerRequestType.Department.Alias)
                                     ).AsQueryable();
                //.Load();  
            }
            data = data.Where(predicate);
            output.TotalCount = data.Count();
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize).AsQueryable();
            }
            foreach (var _include in OtherEntities)
            {
                data.Include(_include).Load();
            }
            // data.Include

            // data = data.Join()
            var listdata = data.ToList();
            var returnedData = Mapper.Map<ICollection<CustomerRequest>, List<RequestViewModel>>(listdata);
            for (int i = 0; i < returnedData.Count; i++)
            {
                returnedData[i].RequestStatus.CreatedDate = listdata[i].RequestStatusHistory.LastOrDefault(x => x.RequestStatusId == listdata[i].RequestStatusId)?.UpdatedDate;
            }
            output.Output = returnedData.OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region Request Update

        protected override void UpdateAbstract(RequestUpdateModel entity)
        {
            var output = new LogicResult<RequestViewModel>();
            var curentCustomerRequest = this.Uow.GetRepository<CustomerRequest>().GetById(entity.Id);
            var customerRequest = Mapper.Map(entity, curentCustomerRequest);
            var data = this.Uow.GetRepository<CustomerRequest>().Update(customerRequest);
            output.Output = Mapper.Map<CustomerRequest, RequestViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RequestUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Request Remove

        protected override void RemoveAbstract(RequestDeleteModel entity)
        {
            var output = new LogicResult<RequestViewModel>();
            var data = Uow.GetRepository<CustomerRequest>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<CustomerRequest>().Remove(data);
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
