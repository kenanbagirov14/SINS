using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.User;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserCreateModel>();
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserUpdateModel>();
            CreateMap<UserViewModel, UserUpdateModel>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<User, UserDeleteModel>();
            CreateMap<UserDeleteModel, User>();
        }
       
    }
}
