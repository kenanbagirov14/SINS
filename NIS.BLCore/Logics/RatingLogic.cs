using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Rating;
using NIS.DALCore.Context;
using NIS.UtilsCore;

namespace NIS.BLCore.Logics
{
    public class RatingLogic : BaseLogic<RatingViewModel, RatingCreateModel, RatingFindModel, RatingUpdateModel, RatingDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public RatingLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Rating Add

        protected override void AddAbstract(RatingCreateModel entity)
        {
            var output = new LogicResult<RatingViewModel>();
            var data = Mapper.Map<RatingCreateModel, Rating>(entity);
            this.Uow.GetRepository<Rating>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Rating, RatingViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<RatingCreateModel> entities)
        {
            var output = new LogicResult<ICollection<RatingViewModel>>();
            var data = Mapper.Map<List<RatingCreateModel>, List<Rating>>(entities);
            var addedEntities = this.Uow.GetRepository<Rating>().AddRange(data);
            output.Output = Mapper.Map<List<Rating>, List<RatingViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Rating Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<RatingViewModel>>();
            var data = this.Uow.GetRepository<Rating>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Rating>, ICollection<RatingViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<RatingViewModel>();
            var data = this.Uow.GetRepository<Rating>().GetById(id);
            output.Output = Mapper.Map<Rating, RatingViewModel>(data);
            Result = output;
        }

        #endregion

        #region Rating Find

        protected override void FindAbstract(RatingFindModel parameters)
        {
            var output = new LogicResult<RatingViewModel>();
            var predicate = PredicateBuilder.True<Rating>();
            if (parameters == null)
            {
                parameters = new RatingFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }

            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<Rating>().Find(predicate);
            output.Output = Mapper.Map<Rating, RatingViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(RatingFindModel parameters)
        {
            var output = new LogicResult<ICollection<RatingViewModel>>();
            var predicate = PredicateBuilder.True<Rating>();
            if (parameters == null)
            {
                parameters = new RatingFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }

            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<Rating>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Rating>, ICollection<RatingViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
        }

        #endregion

        #region Rating Update

        protected override void UpdateAbstract(RatingUpdateModel entity)
        {
            var output = new LogicResult<RatingViewModel>();
            var curentRating = this.Uow.GetRepository<Rating>().GetById(entity.Id);
            var rating = Mapper.Map(entity, curentRating);
            var data = this.Uow.GetRepository<Rating>().Update(rating);
            output.Output = Mapper.Map<Rating, RatingViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(RatingUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Rating Remove

        protected override void RemoveAbstract(RatingDeleteModel entity)
        {
            var output = new LogicResult<RatingViewModel>();
            var data = Uow.GetRepository<Rating>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Rating>().Remove(data);
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
