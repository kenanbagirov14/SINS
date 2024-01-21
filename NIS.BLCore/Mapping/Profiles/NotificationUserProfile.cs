using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.NotificationUser;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class NotificationUserProfile : Profile
    {
        public NotificationUserProfile()
        {
            CreateMap<NotificationUserCreateModel, NotificationUser>();
            CreateMap<NotificationUser, NotificationUserViewModel>();
            CreateMap<Task<NotificationUser>, Task<NotificationUserViewModel>>();
            CreateMap<NotificationUserDto, NotificationUser>();
            CreateMap<NotificationUser, NotificationUserDto>();
            CreateMap<NotificationUser, NotificationUserCreateModel>();
            CreateMap<NotificationUserCreateModel, NotificationUser>();
            CreateMap<NotificationUser, NotificationUserUpdateModel>();
            CreateMap<NotificationUserUpdateModel, NotificationUser>();
            CreateMap<NotificationUser, NotificationUserDeleteModel>();
            CreateMap<NotificationUserDeleteModel, NotificationUser>();
        }

    }
}
