using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.NotificationUser;
using NIS.DALCore.Context;
using NIS.DALCore.Repository;
using NIS.UtilsCore;
using Task = System.Threading.Tasks.Task;

namespace NIS.BLCore.Logics
{
    public class NotificationUserLogic : BaseLogic<NotificationUserViewModel, NotificationUserCreateModel, NotificationUserFindModel, NotificationUserUpdateModel, NotificationUserDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public NotificationUserLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region NotificationUser Add

        protected override void AddAbstract(NotificationUserCreateModel entity)
        {
            var output = new LogicResult<NotificationUserViewModel>();
            var data = Mapper.Map<NotificationUserCreateModel, NotificationUser>(entity);
            var result = this.Uow.GetRepository<NotificationUser>().Add(data);
            output.Output = Mapper.Map<NotificationUser, NotificationUserViewModel>(result);
            Result = output;
        }


        protected override void AddRangeAbstract(List<NotificationUserCreateModel> entities)
        {
            var output = new LogicResult<ICollection<NotificationUserViewModel>>();
            var data = Mapper.Map<List<NotificationUserCreateModel>, List<NotificationUser>>(entities);
            var addedEntities = this.Uow.GetRepository<NotificationUser>().AddRange(data);
            output.Output = Mapper.Map<List<NotificationUser>, List<NotificationUserViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region NotificationUser Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<NotificationUserViewModel>>();
            var data = this.Uow.GetRepository<NotificationUser>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<NotificationUser>, ICollection<NotificationUserViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            output.TotalCount = data.Count();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<NotificationUserViewModel>();
            var data = this.Uow.GetRepository<NotificationUser>().GetById(id);
            output.Output = Mapper.Map<NotificationUser, NotificationUserViewModel>(data);
            Result = output;
        }

        #endregion

        #region NotificationUser Find

        protected override void FindAbstract(NotificationUserFindModel parameters)
        {
            var output = new LogicResult<NotificationUserViewModel>();
            var predicate = PredicateBuilder.True<NotificationUser>();
            if (parameters == null)
            {
                parameters = new NotificationUserFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.NotificationId != null)
            {
                predicate = predicate.And(r => r.NotificationId == parameters.NotificationId);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.Status != null)
            {
                predicate = predicate.And(r => r.Status == parameters.Status);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<NotificationUser>().Find(predicate);
            output.Output = Mapper.Map<NotificationUser, NotificationUserViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(NotificationUserFindModel parameters)
        {
            var output = new LogicResult<ICollection<NotificationUserViewModel>>();
            var predicate = PredicateBuilder.True<NotificationUser>();
            if (parameters == null)
            {
                parameters = new NotificationUserFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.NotificationId != null)
            {
                predicate = predicate.And(r => r.NotificationId == parameters.NotificationId);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.Status != null)
            {
                predicate = predicate.And(r => r.Status == parameters.Status);
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<NotificationUser>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<NotificationUser>, ICollection<NotificationUserViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            output.TotalCount = data.Count();

            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region NotificationUser Update

        protected override void UpdateAbstract(NotificationUserUpdateModel parameters)
        {
            if (parameters.Id == null) return;
            var output = new LogicResult<NotificationUserViewModel>();
            var curentNotificationUser = this.Uow.GetRepository<NotificationUser>().GetById(parameters.Id[0]);
            var NotificationUser = Mapper.Map(parameters, curentNotificationUser);
            var data = this.Uow.GetRepository<NotificationUser>().Update(NotificationUser);
            output.Output = Mapper.Map<NotificationUser, NotificationUserViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(NotificationUserUpdateModel parameters)
        {
            var output = new LogicResult<ICollection<NotificationUserViewModel>>();

            var predicate = PredicateBuilder.True<NotificationUser>();
            if (parameters == null)
            {
                parameters = new NotificationUserUpdateModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => parameters.Id.Contains(r.Id));
            }
            var at = this.Uow.GetRepository<NotificationUser>().FindAll(predicate).ToList();
            for (int i = 0; i < at.Count; i++)
            {
                at[i].Status = parameters.Status;
            }

            var data = this.Uow.GetRepository<NotificationUser>().UpdateRange(at);
            output.Output = Mapper.Map<ICollection<NotificationUser>, ICollection<NotificationUserViewModel>>(data);
            ResultAll = output;
        }

        #endregion

        #region NotificationUser Remove
        protected override void RemoveAbstract(NotificationUserDeleteModel entity)
        {
            var output = new LogicResult<NotificationUserViewModel>();
            var data = Uow.GetRepository<NotificationUser>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<NotificationUser>().Remove(data);
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
