using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NIS.BLCore.Base;
using NIS.BLCore.Extensions;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequestType;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class CustomerRequestTypeLogic : BaseLogic<RequestTypeViewModel, RequestTypeCreateModel, RequestTypeFindModel, RequestTypeUpdateModel, RequestTypeDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        public string[] OtherEntities = new[] { "ParentRequestType", "ChildRequestTypes", "Department", "SourceType" };

        #endregion

        #region Constructor

        public CustomerRequestTypeLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region RequestType Add
        protected override void AddAbstract(RequestTypeCreateModel entity)
        {
            var output = new LogicResult<RequestTypeViewModel>();
            var data = Mapper.Map<RequestTypeCreateModel, CustomerRequestType>(entity);
            this.Uow.GetRepository<CustomerRequestType>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<CustomerRequestType, RequestTypeViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RequestTypeCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RequestTypeViewModel>>();
            var data = Mapper.Map<List<RequestTypeCreateModel>, List<CustomerRequestType>>(entities);
            var addedEntities = this.Uow.GetRepository<CustomerRequestType>().AddRange(data);
            output.Output = Mapper.Map<List<CustomerRequestType>, List<RequestTypeViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region RequestType Get

        protected override void GetAllAbstract(Filter filter)
        { 
            var output = new LogicResult<ICollection<RequestTypeViewModel>>(); 
            var UserDepartments =  User.GetUserDepartments();
            var UserRoles =  User.GetUserRoles();
            var data = this.Uow.GetRepository<CustomerRequestType>().GetAll(OtherEntities);
            //edit
              
            //edit
            //User.GetUserRoles().Contains("tkq") 
            if (GeneralUserId != 0)
            {
                //myedit
                //if (GeneralUserId == 985 || GeneralUserId == 996 || GeneralUserId == 991 ||
                //    GeneralUserId == 997 || GeneralUserId == 998 || GeneralUserId == 999 || GeneralUserId == 1000
                //    || GeneralUserId == 1001 || GeneralUserId == 1002 || GeneralUserId == 1003
                //    || GeneralUserId == 1004 || GeneralUserId == 987 || GeneralUserId == 988 || GeneralUserId == 990
                //    || GeneralUserId == 943
                //    )
                //{
                //    //
                //    System.IO.File.AppendAllText("log.txt", "User........Elshan Imanov.........");
                //    foreach (var usDep in UserDepartments)
                //    {
                //        System.IO.File.AppendAllText("log.txt", "departments..........");
                //        System.IO.File.AppendAllText("log.txt", usDep);
                //    }
                //    //
                //    string dep = "Azercell";
                //    UserDepartments.Clear();
                //    UserDepartments.Add(dep);

                   
                //}
                //myedit

                data = data.Where(
                    x => x.Department.DefaultUserId == GeneralUserId
                 || UserDepartments.Contains(x.Department.Alias)
                 || UserRoles.Contains(x.Alias)
                 //|| 
                 )
                 .Where(x=>x.ParentRequestTypeId==null);
               
            }
            output.TotalCount = data.Count();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<CustomerRequestType>, ICollection<RequestTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RequestTypeViewModel>();
            var data = this.Uow.GetRepository<CustomerRequestType>().GetById(id, OtherEntities);
            output.Output = Mapper.Map<CustomerRequestType, RequestTypeViewModel>(data);
            Result = output;
        }

        #endregion

        #region RequestType Find

        protected override void FindAbstract(RequestTypeFindModel parameters)
        {
            var output = new LogicResult<RequestTypeViewModel>();
            var predicate = PredicateBuilder.True<CustomerRequestType>();
            if (parameters == null)
            {
                parameters = new RequestTypeFindModel();
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
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            var data = this.Uow.GetRepository<CustomerRequestType>().Find(predicate, OtherEntities);
            output.Output = Mapper.Map<CustomerRequestType, RequestTypeViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RequestTypeFindModel parameters)
        {
            var output = new LogicResult<ICollection<RequestTypeViewModel>>();
            var predicate = PredicateBuilder.True<CustomerRequestType>();

            if (parameters == null)
            {
                parameters = new RequestTypeFindModel();
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
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.SourceTypeId != null)
            {
                predicate = predicate.And(r => r.SourceTypeId == parameters.SourceTypeId);
            }
            var data = this.Uow.GetRepository<CustomerRequestType>().FindAll(predicate, OtherEntities);
             
            var UserDepartments = User.GetUserDepartments();
            //if (userId != 0)
            //{
            //    data = data.Where(x => x.Department.DefaultUserId == userId
            //     || UserDepartments.Contains(x.Department.Alias));
            //}
            output.TotalCount = data.Count();

            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<CustomerRequestType>, ICollection<RequestTypeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region RequestType Update

        protected override void UpdateAbstract(RequestTypeUpdateModel entity)
        {
            var output = new LogicResult<RequestTypeViewModel>();
            var curentCustomerRequestType = this.Uow.GetRepository<CustomerRequestType>().GetById(entity.Id);
            var customerRequestType = Mapper.Map(entity, curentCustomerRequestType);
            var data = this.Uow.GetRepository<CustomerRequestType>().Update(customerRequestType);
            output.Output = Mapper.Map<CustomerRequestType, RequestTypeViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RequestTypeUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region RequestType Remove

        protected override void RemoveAbstract(RequestTypeDeleteModel entity)
        {
            var output = new LogicResult<RequestTypeViewModel>();
            var data = Uow.GetRepository<CustomerRequestType>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<CustomerRequestType>().Remove(data);
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
