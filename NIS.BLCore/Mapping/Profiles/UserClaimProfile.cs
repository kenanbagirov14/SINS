using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.UserClaim;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class UserClaimProfile : Profile
    {
        public UserClaimProfile()
        {
            CreateMap<UserClaimCreateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimViewModel>();
            CreateMap<UserClaim, UserClaimDto>();
            CreateMap<UserClaim, UserClaimCreateModel>();
            CreateMap<UserClaimCreateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimUpdateModel>();
            CreateMap<UserClaimViewModel, UserClaimUpdateModel>();
            CreateMap<UserClaimUpdateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimDeleteModel>();
            CreateMap<UserClaimDeleteModel, UserClaim>();
        }
       
    }
}
