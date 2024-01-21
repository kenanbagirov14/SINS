using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.Engineer;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class EngineerProfile : Profile
    {
        public EngineerProfile()
        {
            CreateMap<EngineerCreateModel, Engineer>();
            CreateMap<Engineer, EngineerViewModel>();
            CreateMap<Engineer, EngineerDto>();
            CreateMap<Engineer, EngineerCreateModel>();
            CreateMap<EngineerCreateModel, Engineer>(); 
            CreateMap<Engineer, EngineerUpdateModel>();
            CreateMap<EngineerUpdateModel, Engineer>();
            CreateMap<Engineer, EngineerDeleteModel>();
            CreateMap<EngineerDeleteModel, Engineer>();
        }
       
    }
}
