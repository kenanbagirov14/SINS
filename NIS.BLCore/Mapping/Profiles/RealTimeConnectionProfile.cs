using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.RealTimeConnection;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class RealTimeConnectionProfile : Profile
    {
        public RealTimeConnectionProfile()
        {
            CreateMap<RealTimeConnectionCreateModel, RealTimeConnection>();
            CreateMap<RealTimeConnection, RealTimeConnectionViewModel>();
            CreateMap<RealTimeConnectionViewModel, RealTimeConnection>();
            CreateMap<RealTimeConnection, RealTimeConnectionDto>();
            CreateMap<RealTimeConnection, RealTimeConnectionCreateModel>();
            CreateMap<RealTimeConnectionCreateModel, RealTimeConnection>();
            CreateMap<RealTimeConnection, RealTimeConnectionUpdateModel>();
            CreateMap<RealTimeConnectionUpdateModel, RealTimeConnection>();
            CreateMap<RealTimeConnection, RealTimeConnectionDeleteModel>();
            CreateMap<RealTimeConnectionDeleteModel, RealTimeConnection>();
        }
       
    }
}
