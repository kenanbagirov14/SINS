using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Base;
using NIS.BLCore.Extensions;
using Microsoft.AspNetCore.Http;
using NIS.BLCore.Hubs;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Comment;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.SignalR;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using NIS.BLCore.Helpers;

namespace NIS.BLCore.Logics
{
    public class CommentLogic : BaseLogic<CommentViewModel, CommentCreateModel, CommentFindModel, CommentUpdateModel, CommentDeleteModel>
    {
        #region Properties

        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());
        public string[] OtherEntities = new[] { "Attachment", "User" };
        private readonly IHubContext<NisHub> _hubContext;

        #endregion

        #region Constructor

        public CommentLogic(ClaimsPrincipal User, IHubContext<NisHub> hubContext) : base(User)
        {
            _hubContext = hubContext;
        }

        #endregion


        #region Comment Add

        protected override void AddAbstract(CommentCreateModel entity)
        {

            var output = new LogicResult<CommentViewModel>();
            var data = Mapper.Map<CommentCreateModel, Comment>(entity);
            data.UserId = GeneralUserId;
            var result = this.Uow.GetRepository<Comment>().Add(data);
            this.Uow.SaveChanges();
            var returnedData = Mapper.Map<Comment, CommentViewModel>(result);

            //Send Data to all Clients with SignalR
         
            SignalRHelper.RemoteCommentadd(returnedData, _hubContext);

            //Return Data
            output.Output = returnedData;
            Result = output;
        }

        protected override void AddRangeAbstract(List<CommentCreateModel> parameters)
        {
            var output = new LogicResult<ICollection<CommentViewModel>>();
            var data = Mapper.Map<List<CommentCreateModel>, List<Comment>>(parameters);
            this.Uow.GetRepository<Comment>().AddRange(data);
            this.Uow.SaveChanges();

            output.Output = Mapper.Map<List<Comment>, List<CommentViewModel>>(data);
            ResultAll = output;
        }

        #endregion

        #region Comment Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<CommentViewModel>>();
            var data = this.Uow.GetRepository<Comment>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Comment>, ICollection<CommentViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<CommentViewModel>();
            var data = this.Uow.GetRepository<Comment>().GetById(id, OtherEntities);
            output.Output = Mapper.Map<Comment, CommentViewModel>(data);
            Result = output;
        }

        #endregion

        #region Comment Find

        protected override void FindAbstract(CommentFindModel parameters)
        {
            var output = new LogicResult<CommentViewModel>();
            var predicate = PredicateBuilder.True<Comment>();
            if (parameters == null)
            {
                parameters = new CommentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
            }
            if (parameters.Content != null)
            {
                predicate = predicate.And(r => r.Content.Contains(parameters.Content));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            var data = this.Uow.GetRepository<Comment>().Find(predicate, OtherEntities);
            var returnedData = Mapper.Map<Comment, CommentViewModel>(data);
            output.Output = returnedData;
            Result = output;
        }

        protected override void FindAllAbstract(CommentFindModel parameters)
        {
            var output = new LogicResult<ICollection<CommentViewModel>>();
            var predicate = PredicateBuilder.True<Comment>();
            if (parameters == null)
            {
                parameters = new CommentFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.MainTaskId != null)
            {
                predicate = predicate.And(r => r.MainTaskId == parameters.MainTaskId);
            }
            if (parameters.Content != null)
            {
                predicate = predicate.And(r => r.Content.Contains(parameters.Content));
            }
            if (parameters.CreatedDate != null)
            {
                predicate = predicate.And(r => r.CreatedDate == parameters.CreatedDate);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            var data = this.Uow.GetRepository<Comment>().FindAll(predicate, OtherEntities);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data =
               data.OrderByDescending(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            var returnedData = Mapper.Map<ICollection<Comment>, ICollection<CommentViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            output.Output = returnedData;
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Comment Update

        protected override void UpdateAbstract(CommentUpdateModel entity)
        {
            var output = new LogicResult<CommentViewModel>();
            var curentComment = this.Uow.GetRepository<Comment>().GetById(entity.Id);
            var comment = Mapper.Map(entity, curentComment);
            var data = this.Uow.GetRepository<Comment>().Update(comment);
            output.Output = Mapper.Map<Comment, CommentViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(CommentUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Comment Remove

        protected override void RemoveAbstract(CommentDeleteModel entity)
        {
            var output = new LogicResult<CommentViewModel>();
            var data = Uow.GetRepository<Comment>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Comment>().Remove(data);
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
