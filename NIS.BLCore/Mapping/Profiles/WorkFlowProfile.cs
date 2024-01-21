using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.WorkFlow;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class WorkFlowProfile : Profile
    {
        public WorkFlowProfile()
        {
            CreateMap<WorkFlowCreateModel, WorkFlow>();
            CreateMap<WorkFlow, WorkFlowViewModel>();
            CreateMap<WorkFlow, WorkFlowDto>();
            CreateMap<WorkFlow, WorkFlowCreateModel>();
            CreateMap<WorkFlowCreateModel, WorkFlow>(); 
            CreateMap<WorkFlow, WorkFlowUpdateModel>();
            CreateMap<WorkFlowUpdateModel, WorkFlow>();
            CreateMap<WorkFlow, WorkFlowDeleteModel>();
            CreateMap<WorkFlowDeleteModel, WorkFlow>();
        }
       
    }
}
