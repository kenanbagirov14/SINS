using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.RequestEmail;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class RequestEmailProfile : Profile
    {
        public RequestEmailProfile()
        {
            CreateMap<RequestEmailViewModel, RequestEmail>();
            CreateMap<RequestEmail, RequestEmailViewModel>(); 
            CreateMap<RequestEmail, RequestEmailCreateModel>();
            CreateMap<RequestEmailCreateModel, RequestEmail>();  
        }
       
    }
}
