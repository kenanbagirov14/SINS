using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Node;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
