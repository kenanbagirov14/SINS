using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.InjuryType;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
