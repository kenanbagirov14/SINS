using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.Node;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class NodeProfile : Profile
    {
        public NodeProfile()
        {
            CreateMap<NodeCreateModel, Node>();
            CreateMap<Node, NodeViewModel>();
            CreateMap<Node, NodeDto>();
            CreateMap<Node, NodeCreateModel>();
            CreateMap<NodeCreateModel, Node>(); 
            CreateMap<Node, NodeUpdateModel>();
            CreateMap<NodeUpdateModel, Node>();
            CreateMap<Node, NodeDeleteModel>();
            CreateMap<NodeDeleteModel, Node>();
        }
       
    }
}
