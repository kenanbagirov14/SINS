using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Tag;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class TagLogic : BaseLogic<TagViewModel, TagCreateModel, TagFindModel, TagUpdateModel, TagDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TagLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Tag Add

        protected override void AddAbstract(TagCreateModel entity)
        {
            var output = new LogicResult<TagViewModel>();
            var data = Mapper.Map<TagCreateModel, Tag>(entity);
            this.Uow.GetRepository<Tag>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Tag, TagViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TagCreateModel> entities)
        {
            var output = new LogicResult<ICollection<TagViewModel>>();
            var data = Mapper.Map<List<TagCreateModel>, List<Tag>>(entities);
            var addedEntities = this.Uow.GetRepository<Tag>().AddRange(data);
            output.Output = Mapper.Map<List<Tag>, List<TagViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Tag Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TagViewModel>>();
            var data = this.Uow.GetRepository<Tag>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Tag>, ICollection<TagViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TagViewModel>();
            var data = this.Uow.GetRepository<Tag>().GetById(id);
            output.Output = Mapper.Map<Tag, TagViewModel>(data);
            Result = output;
        }

        #endregion

        #region Tag Find

        protected override void FindAbstract(TagFindModel parameters)
        {
            var output = new LogicResult<TagViewModel>();
            var predicate = PredicateBuilder.True<Tag>();
            if (parameters == null)
            {
                parameters = new TagFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Tag>().Find(predicate);
            output.Output = Mapper.Map<Tag, TagViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(TagFindModel parameters)
        {
            var output = new LogicResult<ICollection<TagViewModel>>();
            var predicate = PredicateBuilder.True<Tag>();
            if (parameters == null)
            {
                parameters = new TagFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Tag>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Tag>, ICollection<TagViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        #endregion

        #region Tag Update

        protected override void UpdateAbstract(TagUpdateModel entity)
        {
            var output = new LogicResult<TagViewModel>();
            var curentTag = this.Uow.GetRepository<Tag>().GetById(entity.Id);
            var tag = Mapper.Map(entity, curentTag);
            var data = this.Uow.GetRepository<Tag>().Update(tag);
            output.Output = Mapper.Map<Tag, TagViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TagUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Tag Remove

        protected override void RemoveAbstract(TagDeleteModel entity)
        {
            var output = new LogicResult<TagViewModel>();
            var data = Uow.GetRepository<Tag>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Tag>().Remove(data);
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
