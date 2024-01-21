using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class AttachmentLogic : BaseLogic<AttachmentViewModel, AttachmentCreateModel, AttachmentFindModel, AttachmentUpdateModel, AttachmentDeleteModel>
    {
        #region Properties

        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        private IHostingEnvironment _env;
        #endregion

        #region Constructor

        public AttachmentLogic(IHostingEnvironment env, ClaimsPrincipal User) : base(User)
        {
            _env = env;
        }

        #endregion


        #region Attachment Add

        protected override void AddAbstract(AttachmentCreateModel entity)
        {
            var output = new LogicResult<AttachmentViewModel>();
            string fileName = Guid.NewGuid().ToString();
            var webRoot = _env.ContentRootPath+"/file/" ;
            string filePathToSave = Path.Combine(webRoot, fileName + "." + entity.Extension);
            string filePath = webRoot + fileName + "." + entity.Extension;
            File.WriteAllBytes(filePathToSave, Convert.FromBase64String(entity.Container));
            entity.FilePath = fileName + '.' + entity.Extension;
            this.Uow.GetRepository<Attachment>().Add(Mapper.Map<AttachmentCreateModel, Attachment>(entity));
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<AttachmentCreateModel, AttachmentViewModel>(entity);
            Result = output;
        }

        protected override void AddRangeAbstract(List<AttachmentCreateModel> parameters)
        {
            var output = new LogicResult<ICollection<AttachmentViewModel>>();
            var data = Mapper.Map<List<AttachmentCreateModel>, List<Attachment>>(parameters);
            var addedEntities = this.Uow.GetRepository<Attachment>().AddRange(data);
            output.Output = Mapper.Map<List<Attachment>, List<AttachmentViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Attachment Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<AttachmentViewModel>>();
            var data = this.Uow.GetRepository<Attachment>().GetAll().ToList();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
               data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            output.Output = Mapper.Map<List<Attachment>, List<AttachmentViewModel>>(data).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<AttachmentViewModel>();
            var data = this.Uow.GetRepository<Attachment>().GetById(id);
            output.Output = Mapper.Map<Attachment, AttachmentViewModel>(data);
            Result = output;
        }

        #endregion

        #region Attachment Find

        protected override void FindAbstract(AttachmentFindModel parameters)
        {
            var output = new LogicResult<AttachmentViewModel>();
            var predicate = PredicateBuilder.True<Attachment>();
            if (parameters == null)
            {
                parameters = new AttachmentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
            }
            if (parameters.CommentId != null)
            {
                predicate = predicate.And(r => r.CommentId == parameters.CommentId);
            }
            if (parameters.FilePath != null)
            {
                predicate = predicate.And(r => r.FilePath.Contains(parameters.FilePath));
            }
            if (parameters.FileType != null)
            {
                predicate = predicate.And(r => r.FileType == parameters.FileType);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Attachment>().Find(predicate);
            output.Output = Mapper.Map<Attachment, AttachmentViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(AttachmentFindModel parameters)
        {
            var output = new LogicResult<ICollection<AttachmentViewModel>>();
            var predicate = PredicateBuilder.True<Attachment>();
            if (parameters == null)
            {
                parameters = new AttachmentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.CustomerRequestId != null)
            {
                predicate = predicate.And(r => r.CustomerRequestId == parameters.CustomerRequestId);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
            }
            if (parameters.CommentId != null)
            {
                predicate = predicate.And(r => r.CommentId == parameters.CommentId);
            }
            if (parameters.FilePath != null)
            {
                predicate = predicate.And(r => r.FilePath.Contains(parameters.FilePath));
            }
            if (parameters.FileType != null)
            {
                predicate = predicate.And(r => r.FileType == parameters.FileType);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Attachment>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Attachment>, ICollection<AttachmentViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        #endregion

        #region Attachment Update

        protected override void UpdateAbstract(AttachmentUpdateModel entity)
        {
            var output = new LogicResult<AttachmentViewModel>();
            var curentAttachment = this.Uow.GetRepository<Attachment>().GetById(entity.Id);
            var attachment = Mapper.Map(entity, curentAttachment);
            var data = this.Uow.GetRepository<Attachment>().Update(attachment);
            output.Output = Mapper.Map<Attachment, AttachmentViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(AttachmentUpdateModel parameter)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Attachment Remove

        protected override void RemoveAbstract(AttachmentDeleteModel entity)
        {
            LogicResult<AttachmentViewModel> output = new LogicResult<AttachmentViewModel>();
            var data = Uow.GetRepository<Area>().GetById(entity.Id);
            this.Uow.GetRepository<Area>().Remove(data);
            Result = output;
        }

        #endregion
    }
}
