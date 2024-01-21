using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Project;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.DALCore.Repository;
using NIS.UtilsCore;
using Task = System.Threading.Tasks.Task;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class ProjectLogic : BaseLogic<ProjectViewModel, ProjectCreateModel, ProjectFindModel, ProjectUpdateModel, ProjectDeleteModel> 
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public ProjectLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Project Add

        protected override void AddAbstract(ProjectCreateModel entity)
        {
            var output = new LogicResult<ProjectViewModel>();
            var data = Mapper.Map<ProjectCreateModel, Project>(entity);
            this.Uow.GetRepository<Project>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Project, ProjectViewModel>(data);
            Result = output;
        }

        #endregion

        #region Project Get


        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<ProjectViewModel>>();
            var data = this.Uow.GetRepository<Project>().GetAll();
            if (filter.PageSize != 0 && filter.PageNumber != 0)
            {
                data = data.OrderBy(x => x.Id).Skip(filter.PageNumber * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Project>, ICollection<ProjectViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<ProjectViewModel>();
            var data = this.Uow.GetRepository<Project>().GetById(id);
            output.Output = Mapper.Map<Project, ProjectViewModel>(data);
            Result = output;
        }

        #endregion

        #region Project Find

        protected override void FindAbstract(ProjectFindModel parameters)
        {
            var output = new LogicResult<ProjectViewModel>();
            var predicate = PredicateBuilder.True<Project>();
            if (parameters == null)
            {
                parameters = new ProjectFindModel();
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
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Project>().Find(predicate);
            output.Output = Mapper.Map<Project, ProjectViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(ProjectFindModel parameters)
        {
            var output = new LogicResult<ICollection<ProjectViewModel>>();
            var predicate = PredicateBuilder.True<Project>();
            if (parameters == null)
            {
                parameters = new ProjectFindModel();
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
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Project>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)(parameters.PageNumber * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Project>, ICollection<ProjectViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region Project Update

        protected override void UpdateAbstract(ProjectUpdateModel entity)
        {
            var output = new LogicResult<ProjectViewModel>();
            var curentProject = this.Uow.GetRepository<Project>().GetById(entity.Id);
            var project = Mapper.Map(entity, curentProject);
            var data = this.Uow.GetRepository<Project>().Update(project);
            output.Output = Mapper.Map<Project, ProjectViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(ProjectUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Project Remove

        protected override void RemoveAbstract(ProjectDeleteModel entity)
        {
            var data = Mapper.Map<ProjectDeleteModel, Project>(entity);
            this.Uow.GetRepository<Project>().Remove(data);
        }

        protected override void AddRangeAbstract(List<ProjectCreateModel> parameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
