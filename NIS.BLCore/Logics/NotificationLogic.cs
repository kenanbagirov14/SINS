using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Notification;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class NotificationLogic : BaseLogic<NotificationViewModel, NotificationCreateModel, NotificationFindModel, NotificationUpdateModel, NotificationDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public NotificationLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Notification Add

        protected override void AddAbstract(NotificationCreateModel parameter)
        {
            LogicResult<NotificationViewModel> output = new LogicResult<NotificationViewModel>();
            var data = Mapper.Map<NotificationCreateModel, Notification>(parameter);
            var result = this.Uow.GetRepository<Notification>().Add(data);
            output.Output = Mapper.Map<Notification, NotificationViewModel>(result);
            Result = output;
        }

        protected override void AddRangeAbstract(List<NotificationCreateModel> parameters)
        {
            LogicResult<ICollection<NotificationViewModel>> output = new LogicResult<ICollection<NotificationViewModel>>();
            var data = Mapper.Map<List<NotificationCreateModel>, List<Notification>>(parameters);
            var addedEntities = Uow.GetRepository<Notification>().AddRange(data);
            output.Output = Mapper.Map<List<Notification>, List<NotificationViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Notification Get

        protected override void GetAllAbstract(Filter filter)
        {
            LogicResult<ICollection<NotificationViewModel>> output = new LogicResult<ICollection<NotificationViewModel>>();

            var data = this.Uow.GetRepository<Notification>().GetAll().ToList();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
                  data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            output.Output = Mapper.Map<List<Notification>, List<NotificationViewModel>>(data).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            LogicResult<NotificationViewModel> output = new LogicResult<NotificationViewModel>();
            var data = this.Uow.GetRepository<Notification>().GetById(id);
            output.Output = Mapper.Map<Notification, NotificationViewModel>(data);
            Result = output;
        }

        #endregion

        #region Notification Find

        protected override void FindAbstract(NotificationFindModel parameters)
        {
            var output = new LogicResult<NotificationViewModel>();
            var predicate = PredicateBuilder.True<Notification>();
            if (parameters == null)
            {
                parameters = new NotificationFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.NotificationTypeId != null)
            {
                predicate = predicate.And(r => r.NotificationTypeId == parameters.NotificationTypeId);
            }
            if (parameters.Content != null)
            {
                predicate = predicate.And(r => r.Content.Contains(parameters.Content));
            }
            if (parameters.Subject != null)
            {
                predicate = predicate.And(r => r.Subject.Contains(parameters.Subject));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Notification>().Find(predicate);
            output.Output = Mapper.Map<Notification, NotificationViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(NotificationFindModel parameters)
        {
            var output = new LogicResult<ICollection<NotificationViewModel>>();
            var predicate = PredicateBuilder.True<Notification>();
            if (parameters == null)
            {
                parameters = new NotificationFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.NotificationTypeId != null)
            {
                predicate = predicate.And(r => r.NotificationTypeId == parameters.NotificationTypeId);
            }
            if (parameters.Content != null)
            {
                predicate = predicate.And(r => r.Content.Contains(parameters.Content));
            }
            if (parameters.Subject != null)
            {
                predicate = predicate.And(r => r.Subject.Contains(parameters.Subject));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Notification>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Notification>, ICollection<NotificationViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion


        #region Notification Update

        protected override void UpdateAbstract(NotificationUpdateModel parameter)
        {
            LogicResult<NotificationViewModel> output = new LogicResult<NotificationViewModel>();
            var curentNotification = this.Uow.GetRepository<Notification>().GetById(parameter.Id);
            var notification = Mapper.Map(parameter, curentNotification);
            var data = this.Uow.GetRepository<Notification>().Update(notification);
            output.Output = Mapper.Map<Notification, NotificationViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(NotificationUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Notification Remove

        protected override void RemoveAbstract(NotificationDeleteModel entity)
        {
            LogicResult<NotificationViewModel> output = new LogicResult<NotificationViewModel>();
            var notification = Uow.GetRepository<Notification>().GetById(entity.Id);
            this.Uow.GetRepository<Notification>().Remove(notification);
            Result = output;
        }

        #endregion
    }
}
