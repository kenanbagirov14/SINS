using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.RequestStatus;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class RequestStatusProfile : Profile
    {
        public RequestStatusProfile()
        {
            CreateMap<RequestStatusCreateModel, RequestStatus>();
            CreateMap<RequestStatus, RequestStatusViewModel>();
            CreateMap<RequestStatus, RequestStatusDto>();
            CreateMap<RequestStatus, RequestStatusCreateModel>();
            CreateMap<RequestStatusCreateModel, RequestStatus>(); 
            CreateMap<RequestStatus, RequestStatusUpdateModel>();
            CreateMap<RequestStatusUpdateModel, RequestStatus>();
            CreateMap<RequestStatus, RequestStatusDeleteModel>();
            CreateMap<RequestStatusDeleteModel, RequestStatus>();
        }
       
    }
}
