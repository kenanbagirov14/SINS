using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Area;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<AreaCreateModel, Area>();
            CreateMap<Area, AreaViewModel>();
            CreateMap<Area, AreaDto>();
            CreateMap<Area, AreaCreateModel>();
            CreateMap<AreaCreateModel, Area>();
            CreateMap<Area, AreaUpdateModel>();
            CreateMap<AreaUpdateModel, Area>();
            CreateMap<Area, AreaDeleteModel>();
            CreateMap<AreaDeleteModel, Area>();
        }
       
    }
}
