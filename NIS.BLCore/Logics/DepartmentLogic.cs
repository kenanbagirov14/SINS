
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Extensions;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Department;
using NIS.DALCore.Context;
using NIS.UtilsCore;
namespace NIS.BLCore.Logics
{
    public class DepartmentLogic : BaseLogic<DepartmentViewModel, DepartmentCreateModel, DepartmentFindModel, DepartmentUpdateModel, DepartmentDeleteModel>
    {
        #region Properties
        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        public string[] OtherEntities = new[] { "ChildDepartments" };
        #endregion

        #region Constructor
        public DepartmentLogic(ClaimsPrincipal User) : base(User)
        {
        }
        #endregion

        #region Department Add
        protected override void AddAbstract(DepartmentCreateModel entity)
        {
            var output = new LogicResult<DepartmentViewModel>();
            var data = Mapper.Map<DepartmentCreateModel, Department>(entity);
            this.Uow.GetRepository<Department>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Department, DepartmentViewModel>(data);
            Result = output;
        }
        protected override void AddRangeAbstract(List<DepartmentCreateModel> entities)
        {
            var output = new LogicResult<ICollection<DepartmentViewModel>>();
            var data = Mapper.Map<List<DepartmentCreateModel>, List<Department>>(entities);
            var addedEntities = this.Uow.GetRepository<Department>().AddRange(data);
            output.Output = Mapper.Map<List<Department>, List<DepartmentViewModel>>(addedEntities);
            ResultAll = output;
        }
        #endregion

        #region Department Get
        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<DepartmentViewModel>>();
            var UserDepartments = User.GetUserDepartments();
            var data = this.Uow.GetRepository<Department>().GetAll(OtherEntities).Where(x => x.ParentDepartmentId == null);
            //edit
            //if (GeneralUserId == 985 || GeneralUserId == 996 || GeneralUserId == 997 || GeneralUserId == 998
            //    || GeneralUserId == 987 || GeneralUserId == 988 || GeneralUserId == 990 || GeneralUserId == 991
            //    || GeneralUserId == 999 || GeneralUserId == 1000 || GeneralUserId == 1001 || GeneralUserId == 1002
            //   || GeneralUserId == 1003 || GeneralUserId == 1004)
            //{
            //    data = this.Uow.GetRepository<Department>().GetAll(OtherEntities).Where(x => x.ParentDepartmentId == 2476);
            //}
            if (GeneralUserId == 943)
            {
                //nazirliye kimi 
                data = this.Uow.GetRepository<Department>().GetAll(OtherEntities).Where(x => x.ParentDepartmentId == 2477);
            }
            //edit
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
               data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            data = data.Where(w => w.Alias != null);
       
            output.Output = Mapper.Map<List<Department>, List<DepartmentViewModel>>(data.ToList()).OrderBy(x => x.Name).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }
        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<DepartmentViewModel>();
            var data = this.Uow.GetRepository<Department>().GetById(id);
            output.Output = Mapper.Map<Department, DepartmentViewModel>(data);
            Result = output;
        }
        #endregion

        #region Department Find
        protected override void FindAbstract(DepartmentFindModel parameters)
        {
            var output = new LogicResult<DepartmentViewModel>();
            var predicate = PredicateBuilder.True<Department>();
            if (parameters == null)
            {
                parameters = new DepartmentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.DepartmentType != null)
            {
                predicate = predicate.And(r => r.DepartmentType.Contains(parameters.DepartmentType));
            }
            if (parameters.DepartmentPrefix != null)
            {
                predicate = predicate.And(r => r.DepartmentPrefix == parameters.DepartmentPrefix);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.DefaultUserId != null)
            {
                predicate = predicate.And(r => r.DefaultUserId == parameters.DefaultUserId);
            }
            if (parameters.Alias != null)
            {
                predicate = predicate.And(r => r.Alias.ToLower() == parameters.Alias.ToLower());
            }
            var data = this.Uow.GetRepository<Department>().Find(predicate);
            output.Output = Mapper.Map<Department, DepartmentViewModel>(data);
            Result = output;
        }
        protected override void FindAllAbstract(DepartmentFindModel parameters)
        {
            var output = new LogicResult<ICollection<DepartmentViewModel>>();
            var predicate = PredicateBuilder.True<Department>();
            if (parameters == null)
            {
                parameters = new DepartmentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.DepartmentType != null)
            {
                predicate = predicate.And(r => r.DepartmentType.Contains(parameters.DepartmentType));
            }
            if (parameters.DepartmentPrefix != null)
            {
                predicate = predicate.And(r => r.DepartmentPrefix == parameters.DepartmentPrefix);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            if (parameters.DefaultUserId != null)
            {
                predicate = predicate.And(r => r.DefaultUserId == parameters.DefaultUserId);
            }
            var UserDepartments = User.GetUserDepartments();
            var UserRoles = User.GetUserRoles();
            var data = this.Uow.GetRepository<Department>().FindAll(predicate);
            if (GeneralUserId != 0 && !UserRoles.Contains("allDepartment"))
            {
                data = data.Where(x => x.DefaultUserId == GeneralUserId
                 || UserDepartments.Contains(x.Alias));
            }
            var ot = data;
            data = data.Where(a => ot.FirstOrDefault(x => x.Id == a.ParentDepartmentId) == null);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Department>, ICollection<DepartmentViewModel>>(data.ToList()).OrderBy(x => x.Name).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }
        #endregion

        #region Department Update
        protected override void UpdateAbstract(DepartmentUpdateModel entity)
        {
            var output = new LogicResult<DepartmentViewModel>();
            var curentDepartment = this.Uow.GetRepository<Department>().GetById(entity.Id);
            var department = Mapper.Map(entity, curentDepartment);
            var data = this.Uow.GetRepository<Department>().Update(department);
            output.Output = Mapper.Map<Department, DepartmentViewModel>(data);
            Result = output;
        }
        protected override void UpdateRangeAbstract(DepartmentUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Department Remove
        protected override void RemoveAbstract(DepartmentDeleteModel entity)
        {
            var output = new LogicResult<DepartmentViewModel>();
            var data = Uow.GetRepository<Department>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Department>().Remove(data);
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