using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.Area;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
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
