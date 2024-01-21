using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.TaskStatus;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
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
