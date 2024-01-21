using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Engineer;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
