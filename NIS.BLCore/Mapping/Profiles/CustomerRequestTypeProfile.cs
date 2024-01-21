using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.CustomerRequestType;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
