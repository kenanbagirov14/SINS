using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.RequestStatusHistory;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
