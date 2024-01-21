using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.UserClaim;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class UserClaimLogic : BaseLogic<UserClaimViewModel, UserClaimCreateModel, UserClaimFindModel, UserClaimUpdateModel, UserClaimDeleteModel>
    {
        #region Properties

        public NISContext NisContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public UserClaimLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region UserClaim Add

        protected override void AddAbstract(UserClaimCreateModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = Mapper.Map<UserClaimCreateModel, UserClaim>(entity);
            this.Uow.GetRepository<UserClaim>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<UserClaimCreateModel> entities)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var data = Mapper.Map<List<UserClaimCreateModel>, List<UserClaim>>(entities);
            var addedEntities = this.Uow.GetRepository<UserClaim>().AddRange(data);
            output.Output = Mapper.Map<List<UserClaim>, List<UserClaimViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region UserClaim Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var data = this.Uow.GetRepository<UserClaim>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserClaim>, ICollection<UserClaimViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = this.Uow.GetRepository<UserClaim>().GetById(id);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        #endregion

        #region UserClaim Find

        protected override void FindAbstract(UserClaimFindModel parameters)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var predicate = PredicateBuilder.True<UserClaim>();
            if (parameters == null)
            {
                parameters = new UserClaimFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ClaimType != null)
            {
                predicate = predicate.And(r => r.ClaimType.Contains(parameters.ClaimType));
            }
            if (parameters.ClaimValue != null)
            {
                predicate = predicate.And(r => r.ClaimValue.Contains(parameters.ClaimValue));
            }

            var data = this.Uow.GetRepository<UserClaim>().Find(predicate);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(UserClaimFindModel parameters)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var predicate = PredicateBuilder.True<UserClaim>();
            if (parameters == null)
            {
                parameters = new UserClaimFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ClaimType != null)
            {
                predicate = predicate.And(r => r.ClaimType.Contains(parameters.ClaimType));
            }
            if (parameters.ClaimValue != null)
            {
                predicate = predicate.And(r => r.ClaimValue.Contains(parameters.ClaimValue));
            }
            var data = this.Uow.GetRepository<UserClaim>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserClaim>, ICollection<UserClaimViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region UserClaim Update

        protected override void UpdateAbstract(UserClaimUpdateModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var curentUserClaim = this.Uow.GetRepository<UserClaim>().GetById(entity.Id);
            var userClaim = Mapper.Map(entity, curentUserClaim);
            var data = this.Uow.GetRepository<UserClaim>().Update(userClaim);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(UserClaimUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region UserClaim Remove

        protected override void RemoveAbstract(UserClaimDeleteModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = Uow.GetRepository<UserClaim>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<UserClaim>().Remove(data);
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


        public void RemoveRange(List<UserClaimViewModel> parameters)
        {
            var entities = this.Uow.GetRepository<UserClaim>().FindAll(s => parameters.Select(a => a.Id).Contains(s.Id)).ToList();
            this.Uow.GetRepository<UserClaim>().RemoveRange(entities);
        }
        #endregion
    }
}
