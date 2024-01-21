using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NIS.BLCore.Base;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Node;
using NIS.BLCore.Models.Core;
using NIS.DALCore.Context;
using NIS.UtilsCore;
using System.Security.Claims;

namespace NIS.BLCore.Logics
{
    public class NodeLogic : BaseLogic<NodeViewModel, NodeCreateModel, NodeFindModel, NodeUpdateModel, NodeDeleteModel>
    {
        #region Properties
         
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public NodeLogic(ClaimsPrincipal User) : base(User)
        {
        }

        #endregion


        #region Node Add

        protected override void AddAbstract(NodeCreateModel parameter)
        {
            LogicResult<NodeViewModel> output = new LogicResult<NodeViewModel>();
            var data = Mapper.Map<NodeCreateModel, Node>(parameter);
            this.Uow.GetRepository<Node>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Node, NodeViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<NodeCreateModel> parameters)
        {
            LogicResult<ICollection<NodeViewModel>> output = new LogicResult<ICollection<NodeViewModel>>();
            var data = Mapper.Map<List<NodeCreateModel>, List<Node>>(parameters);
            var addedEntities = Uow.GetRepository<Node>().AddRange(data);
            output.Output = Mapper.Map<List<Node>, List<NodeViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Node Get

        protected override void GetAllAbstract(Filter filter)
        {
            LogicResult<ICollection<NodeViewModel>> output = new LogicResult<ICollection<NodeViewModel>>();

            var data = this.Uow.GetRepository<Node>().GetAll().ToList();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data =
                  data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            output.Output = Mapper.Map<List<Node>, List<NodeViewModel>>(data).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            LogicResult<NodeViewModel> output = new LogicResult<NodeViewModel>();
            var data = this.Uow.GetRepository<Node>().GetById(id);
            output.Output = Mapper.Map<Node, NodeViewModel>(data);
            Result = output;
        }

        #endregion

        #region Node Find

        protected override void FindAbstract(NodeFindModel parameters)
        {
            LogicResult<NodeViewModel> output = new LogicResult<NodeViewModel>();
            var predicate = PredicateBuilder.True<Node>();
            if (parameters == null)
            {
                parameters = new NodeFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.AreaId != null)
            {
                predicate = predicate.And(r => r.AreaId == parameters.AreaId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Node>().Find(predicate);
            output.Output = Mapper.Map<Node, NodeViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(NodeFindModel parameters)
        {
            LogicResult<ICollection<NodeViewModel>> output = new LogicResult<ICollection<NodeViewModel>>(); var predicate = PredicateBuilder.True<Node>();
            if (parameters == null)
            {
                parameters = new NodeFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.AreaId != null)
            {
                predicate = predicate.And(r => r.AreaId == parameters.AreaId);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description == parameters.Description);
            }
            var data = this.Uow.GetRepository<Node>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Node>, ICollection<NodeViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Node Update

        protected override void UpdateAbstract(NodeUpdateModel parameter)
        {
            LogicResult<NodeViewModel> output = new LogicResult<NodeViewModel>();
            var curentNode = this.Uow.GetRepository<Node>().GetById(parameter.Id);
            var node = Mapper.Map(parameter, curentNode);
            var data = this.Uow.GetRepository<Node>().Update(node);
            output.Output = Mapper.Map<Node, NodeViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(NodeUpdateModel parameter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Node Remove

        protected override void RemoveAbstract(NodeDeleteModel entity)
        {
            LogicResult<NodeViewModel> output = new LogicResult<NodeViewModel>();
            var node = Uow.GetRepository<Node>().GetById(entity.Id);
            this.Uow.GetRepository<Node>().Remove(node);
            Result = output;
        }

        #endregion

    }
}
