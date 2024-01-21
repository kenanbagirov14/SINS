using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Region;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<RegionCreateModel, Region>();
            CreateMap<Region, RegionViewModel>();
            CreateMap<Region, RegionDto>();
            CreateMap<Region, RegionCreateModel>();
            CreateMap<RegionCreateModel, Region>();
            CreateMap<Region, RegionUpdateModel>();
            CreateMap<RegionUpdateModel, Region>();
            CreateMap<Region, RegionDeleteModel>();
            CreateMap<RegionDeleteModel, Region>();
        }
       
    }
}
