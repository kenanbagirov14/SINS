using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.LawProcess;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class LawProcessProfile : Profile
    {
        public LawProcessProfile()
        {
            CreateMap<LawProcessCreateModel, LawProcess>();
            CreateMap<LawProcess, LawProcessViewModel>();
            CreateMap<LawProcess, LawProcessDto>();
            CreateMap<LawProcess, LawProcessCreateModel>();
            CreateMap<LawProcessCreateModel, LawProcess>(); 
            CreateMap<LawProcess, LawProcessUpdateModel>();
            CreateMap<LawProcessUpdateModel, LawProcess>();
            CreateMap<LawProcess, LawProcessDeleteModel>();
            CreateMap<LawProcessDeleteModel, LawProcess>();
        }
       
    }
}
