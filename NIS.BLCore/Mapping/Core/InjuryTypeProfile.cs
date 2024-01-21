using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.InjuryType;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class InjuryTypeProfile : Profile
    {
        public InjuryTypeProfile()
        {
            CreateMap<InjuryTypeCreateModel, InjuryType>();
            CreateMap<InjuryType, InjuryTypeViewModel>();
            CreateMap<InjuryType, InjuryTypeDto>();
            CreateMap<InjuryType, InjuryTypeCreateModel>();
            CreateMap<InjuryTypeCreateModel, InjuryType>();
            CreateMap<InjuryType, InjuryTypeUpdateModel>();
            CreateMap<InjuryTypeUpdateModel, InjuryType>();
            CreateMap<InjuryType, InjuryTypeDeleteModel>();
            CreateMap<InjuryTypeDeleteModel, InjuryType>();
        }
       
    }
}
