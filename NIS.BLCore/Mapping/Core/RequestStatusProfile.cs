using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.RequestStatus;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
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
