using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.TaskStatus;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class TaskStatusProfile : Profile
    {
        public TaskStatusProfile()
        {
            CreateMap<TaskStatusCreateModel, TaskStatus>();
            CreateMap<TaskStatus, TaskStatusViewModel>();
            CreateMap<TaskStatus, TaskStatusDto>();
            CreateMap<TaskStatus, TaskStatusCreateModel>();
            CreateMap<TaskStatusCreateModel, TaskStatus>(); 
            CreateMap<TaskStatus, TaskStatusUpdateModel>();
            CreateMap<TaskStatusUpdateModel, TaskStatus>();
            CreateMap<TaskStatus, TaskStatusDeleteModel>();
            CreateMap<TaskStatusDeleteModel, TaskStatus>();
        }
       
    }
}
