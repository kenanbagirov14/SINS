using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.CustomerRequest;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class CustomerRequestProfile : Profile
    {
        public CustomerRequestProfile()
        {
            CreateMap<RequestCreateModel, CustomerRequest>();
            CreateMap<CustomerRequest, RequestViewModel>();
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
