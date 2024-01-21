using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.RequestStatusHistory;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class RequestStatusHistoryProfile : Profile
    {
        public RequestStatusHistoryProfile()
        {
            CreateMap<RequestStatusHistoryCreateModel, RequestStatusHistory>();
            CreateMap<RequestStatusHistory, RequestStatusHistoryViewModel>();
            CreateMap<RequestStatusHistory, RequestStatusHistoryDto>();
            CreateMap<RequestStatusHistory, RequestStatusHistoryCreateModel>();
            CreateMap<RequestStatusHistoryCreateModel, RequestStatusHistory>(); 
            CreateMap<RequestStatusHistory, RequestStatusHistoryUpdateModel>();
            CreateMap<RequestStatusHistoryUpdateModel, RequestStatusHistory>();
            CreateMap<RequestStatusHistory, RequestStatusHistoryDeleteModel>();
            CreateMap<RequestStatusHistoryDeleteModel, RequestStatusHistory>();
        }
       
    }
}
