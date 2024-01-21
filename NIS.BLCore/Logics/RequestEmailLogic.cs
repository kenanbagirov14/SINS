using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Project;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.BLCore.Models.RequestEmail;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class RequestEmailLogic : BaseLogic<RequestEmailViewModel, RequestEmailCreateModel, ProjectFindModel, ProjectUpdateModel, ProjectDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public RequestEmailLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Project Add

        protected override void AddAbstract(RequestEmailCreateModel entity)
        {
            var output = new LogicResult<RequestEmailViewModel>();
            var data = Mapper.Map<RequestEmailCreateModel, RequestEmail>(entity);
            this.Uow.GetRepository<RequestEmail>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<RequestEmail, RequestEmailViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RequestEmailCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RequestEmailViewModel>>();
            var data = Mapper.Map<List<RequestEmailCreateModel>, List<RequestEmail>>(entities);
            var addedEntities = this.Uow.GetRepository<RequestEmail>().AddRange(data);
            output.Output = Mapper.Map<List<RequestEmail>, List<RequestEmailViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region RequestEmail Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RequestEmailViewModel>>();
            var data = this.Uow.GetRepository<RequestEmail>().GetAll();
            if (filter.PageSize != 0 && filter.PageNumber != 0)
            {
                data = data.OrderBy(x => x.Id).Skip(filter.PageNumber * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<RequestEmail>, ICollection<RequestEmailViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RequestEmailViewModel>();
            var data = this.Uow.GetRepository<RequestEmail>().GetById(id);
            output.Output = Mapper.Map<RequestEmail, RequestEmailViewModel>(data);
            Result = output;
        }

        #endregion

        #region Project Find

        protected override void FindAbstract(ProjectFindModel parameter)
        {
            throw new System.NotImplementedException();
        }

        protected override void FindAllAbstract(ProjectFindModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Project Update
        protected override void UpdateAbstract(ProjectUpdateModel parameter)
        {
            throw new System.NotImplementedException();
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

        #endregion
    }
}
