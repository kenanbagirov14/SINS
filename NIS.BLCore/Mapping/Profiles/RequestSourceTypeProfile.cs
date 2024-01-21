using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.SourceType;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class SourceTypeProfile : Profile
    {
        public SourceTypeProfile()
        {
            CreateMap<SourceTypeCreateModel, SourceType>();
            CreateMap<SourceType, SourceTypeViewModel>();
            CreateMap<SourceType, SourceTypeDto>();
            CreateMap<SourceType, SourceTypeCreateModel>();
            CreateMap<SourceTypeCreateModel, SourceType>(); 
            CreateMap<SourceType, SourceTypeUpdateModel>();
            CreateMap<SourceTypeUpdateModel, SourceType>();
            CreateMap<SourceType, SourceTypeDeleteModel>();
            CreateMap<SourceTypeDeleteModel, SourceType>();
        }
       
    }
}
