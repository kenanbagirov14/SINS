using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.CustomerRequest;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class CustomerRequestProfile : Profile
    {
        public CustomerRequestProfile()
        {
            CreateMap<RequestCreateModel, CustomerRequest>();
            CreateMap<CustomerRequest, RequestViewModel>();
            CreateMap<RequestCreateModel, RequestViewModel>();
            CreateMap<CustomerRequest, RequestDto>();
            CreateMap<CustomerRequest, RequestCreateModel>();
            CreateMap<RequestCreateModel, CustomerRequest>();
            CreateMap<CustomerRequest, RequestUpdateModel>();
            CreateMap<RequestUpdateModel, CustomerRequest>();
            CreateMap<CustomerRequest, RequestDeleteModel>();
            CreateMap<RequestDeleteModel, CustomerRequest>();
        }
       
    }
}
