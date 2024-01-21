using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.CustomerRequestType;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class CustomerRequestTypeProfile : Profile
    {
        public CustomerRequestTypeProfile()
        {
            CreateMap<RequestTypeCreateModel, CustomerRequestType>();
            CreateMap<CustomerRequestType, RequestTypeViewModel>();
            CreateMap<CustomerRequestType, RequestTypeDto>();
            CreateMap<CustomerRequestType, RequestTypeCreateModel>();
            CreateMap<RequestTypeCreateModel, CustomerRequestType>();
            CreateMap<CustomerRequestType, RequestTypeUpdateModel>();
            CreateMap<RequestTypeUpdateModel, CustomerRequestType>();
            CreateMap<CustomerRequestType, RequestTypeDeleteModel>();
            CreateMap<RequestTypeDeleteModel, CustomerRequestType>();
        }
       
    }
}
