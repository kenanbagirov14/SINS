using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Notification;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
  public class NotificationProfile : Profile
  {
    public NotificationProfile()
    {
      CreateMap<NotificationCreateModel, Notification>();
      CreateMap<Notification, NotificationViewModel>();
      CreateMap<Task<Notification>, Task<NotificationViewModel>>();
      CreateMap<NotificationDto, Notification>();
      CreateMap<NotificationType, NotificationTypeDto>();
      CreateMap<NotificationTypeDto, NotificationType>();
      CreateMap<Notification, NotificationDto>();
      CreateMap<Notification, NotificationCreateModel>();
      CreateMap<NotificationCreateModel, Notification>();
      CreateMap<Notification, NotificationUpdateModel>();
      CreateMap<NotificationUpdateModel, Notification>();
      CreateMap<Notification, NotificationDeleteModel>();
      CreateMap<NotificationDeleteModel, Notification>();
    }

  }
}
