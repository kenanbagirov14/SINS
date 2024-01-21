using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.UserSettings;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class UserSettingsProfile : Profile
    {
        public UserSettingsProfile()
        {
            CreateMap<UserSettingsCreateModel, UserSettings>();
            CreateMap<UserSettings, UserSettingsViewModel>();
            CreateMap<UserSettings, UserSettingsDto>();
            CreateMap<UserSettings, UserSettingsCreateModel>();
            CreateMap<UserSettingsCreateModel, UserSettings>();
            CreateMap<UserSettings, UserSettingsUpdateModel>();
            CreateMap<UserSettingsUpdateModel, UserSettings>();
            CreateMap<UserSettings, UserSettingsDeleteModel>();
            CreateMap<UserSettingsDeleteModel, UserSettings>();
        }
       
    }
}
