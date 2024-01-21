using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.PhoneNumber;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class PhoneNumberLogic : BaseLogic<PhoneNumberViewModel, PhoneNumberCreateModel, PhoneNumberFindModel, PhoneNumberUpdateModel, PhoneNumberDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public PhoneNumberLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region PhoneNumber Add

        protected override void AddAbstract(PhoneNumberCreateModel entity)
        {
            var output = new LogicResult<PhoneNumberViewModel>();
            var data = Mapper.Map<PhoneNumberCreateModel, PhoneNumber>(entity);
            this.Uow.GetRepository<PhoneNumber>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<PhoneNumberCreateModel> entities)
        {
            var output = new LogicResult<ICollection<PhoneNumberViewModel>>();
            var data = Mapper.Map<List<PhoneNumberCreateModel>, List<PhoneNumber>>(entities);
            var addedEntities = this.Uow.GetRepository<PhoneNumber>().AddRange(data);
            output.Output = Mapper.Map<List<PhoneNumber>, List<PhoneNumberViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region PhoneNumber Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<PhoneNumberViewModel>>();
            var data = this.Uow.GetRepository<PhoneNumber>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<PhoneNumber>, ICollection<PhoneNumberViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<PhoneNumberViewModel>();
            var data = this.Uow.GetRepository<PhoneNumber>().GetById(id);
            output.Output = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(data);
            Result = output;
        }

        #endregion

        #region PhoneNumber Find

        protected override void FindAbstract(PhoneNumberFindModel parameters)
        {
            var output = new LogicResult<PhoneNumberViewModel>();
            var predicate = PredicateBuilder.True<PhoneNumber>();
            if (parameters == null)
            {
                parameters = new PhoneNumberFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.SubscriberId != null)
            {
                predicate = predicate.And(r => r.SubscriberId == parameters.SubscriberId);
            }
            if (parameters.Number != null)
            {
                predicate = predicate.And(r => r.Number.Contains(parameters.Number));
            }
            if (parameters.ProviderName != null)
            {
                predicate = predicate.And(r => r.ProviderName.Contains(parameters.ProviderName));
            }
            if (parameters.ADSL != null)
            {
                predicate = predicate.And(r => r.ADSL.Contains(parameters.ADSL));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<PhoneNumber>().Find(predicate);
            output.Output = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(PhoneNumberFindModel parameters)
        {
            var output = new LogicResult<ICollection<PhoneNumberViewModel>>();
            var predicate = PredicateBuilder.True<PhoneNumber>();
            if (parameters == null)
            {
                parameters = new PhoneNumberFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.SubscriberId != null)
            {
                predicate = predicate.And(r => r.SubscriberId == parameters.SubscriberId);
            }
            if (parameters.Number != null)
            {
                predicate = predicate.And(r => r.Number.Contains(parameters.Number));
            }
            if (parameters.ProviderName != null)
            {
                predicate = predicate.And(r => r.ProviderName.Contains(parameters.ProviderName));
            }
            if (parameters.ADSL != null)
            {
                predicate = predicate.And(r => r.ADSL.Contains(parameters.ADSL));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<PhoneNumber>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<PhoneNumber>, ICollection<PhoneNumberViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();
        }

        #endregion

        #region PhoneNumber Update

        protected override void UpdateAbstract(PhoneNumberUpdateModel entity)
        {
            var output = new LogicResult<PhoneNumberViewModel>();
            var curentPhoneNumber = this.Uow.GetRepository<PhoneNumber>().GetById(entity.Id);
            var injuryType = Mapper.Map(entity, curentPhoneNumber);
            var data = this.Uow.GetRepository<PhoneNumber>().Update(injuryType);
            output.Output = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(PhoneNumberUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PhoneNumber Remove

        protected override void RemoveAbstract(PhoneNumberDeleteModel entity)
        {
            var output = new LogicResult<PhoneNumberViewModel>();
            var data = Uow.GetRepository<PhoneNumber>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<PhoneNumber>().Remove(data);
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
