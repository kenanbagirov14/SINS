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
using NIS.BLCore.Models.UserSettings;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class UserSettingsLogic : BaseLogic<UserSettingsViewModel, UserSettingsCreateModel, UserSettingsFindModel, UserSettingsUpdateModel, UserSettingsDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public UserSettingsLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region User Settings Add

        protected override void AddAbstract(UserSettingsCreateModel entity)
        {
            var output = new LogicResult<UserSettingsViewModel>();
            var data = Mapper.Map<UserSettingsCreateModel, UserSettings>(entity);
            data.UserId = GeneralUserId;
            this.Uow.GetRepository<UserSettings>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<UserSettings, UserSettingsViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<UserSettingsCreateModel> entities)
        {
            var output = new LogicResult<ICollection<UserSettingsViewModel>>();
            var data = Mapper.Map<List<UserSettingsCreateModel>, List<UserSettings>>(entities);
            data.ForEach(a => a.UserId = (int)GeneralUserId);
            var addedEntities = this.Uow.GetRepository<UserSettings>().AddRange(data);
            output.Output = Mapper.Map<List<UserSettings>, List<UserSettingsViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region User Settings Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<UserSettingsViewModel>>();
            var data = this.Uow.GetRepository<UserSettings>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserSettings>, ICollection<UserSettingsViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<UserSettingsViewModel>();
            var data = this.Uow.GetRepository<UserSettings>().GetById(id);
            output.Output = Mapper.Map<UserSettings, UserSettingsViewModel>(data);
            Result = output;
        }

        #endregion

        #region User Settings Find

        protected override void FindAbstract(UserSettingsFindModel parameters)
        {
            var output = new LogicResult<UserSettingsViewModel>();
            var predicate = PredicateBuilder.True<UserSettings>();
            if (parameters == null)
            {
                parameters = new UserSettingsFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.Type != null)
            {
                predicate = predicate.And(r => r.Type == parameters.Type);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name.ToLower()));
            }
            if (parameters.Settings != null)
            {
                predicate = predicate.And(r => r.Settings.Contains(parameters.Settings.ToLower()));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<UserSettings>().Find(predicate);
            output.Output = Mapper.Map<UserSettings, UserSettingsViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(UserSettingsFindModel parameters)
        {
            var output = new LogicResult<ICollection<UserSettingsViewModel>>();
            var predicate = PredicateBuilder.True<UserSettings>();
            if (parameters == null)
            {
                parameters = new UserSettingsFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.Type != null)
            {
                predicate = predicate.And(r => r.Type == parameters.Type);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name.ToLower()));
            }
            if (parameters.Settings != null)
            {
                predicate = predicate.And(r => r.Settings.Contains(parameters.Settings.ToLower()));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<UserSettings>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserSettings>, ICollection<UserSettingsViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region User Settings Update

        protected override void UpdateAbstract(UserSettingsUpdateModel entity)
        {
            var output = new LogicResult<UserSettingsViewModel>();
            var curentUserSettings = this.Uow.GetRepository<UserSettings>().GetById(entity.Id);
            var UserSettings = Mapper.Map(entity, curentUserSettings);
            var data = this.Uow.GetRepository<UserSettings>().Update(UserSettings);
            output.Output = Mapper.Map<UserSettings, UserSettingsViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(UserSettingsUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region User Settings Remove

        protected override void RemoveAbstract(UserSettingsDeleteModel entity)
        {
            var output = new LogicResult<UserSettingsViewModel>();
            var data = Uow.GetRepository<UserSettings>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<UserSettings>().Remove(data);
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
