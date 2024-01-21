using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.TaskStatusHistory;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class TaskStatusHistoryProfile : Profile
    {
        public TaskStatusHistoryProfile()
        {
            CreateMap<TaskStatusHistoryCreateModel, TaskStatusHistory>();
            CreateMap<TaskStatusHistory, TaskStatusHistoryViewModel>();
            CreateMap<TaskStatusHistory, TaskStatusHistoryDto>();
            CreateMap<TaskStatusHistory, TaskStatusHistoryCreateModel>();
            CreateMap<TaskStatusHistoryCreateModel, TaskStatusHistory>(); 
            CreateMap<TaskStatusHistory, TaskStatusHistoryUpdateModel>();
            CreateMap<TaskStatusHistoryUpdateModel, TaskStatusHistory>();
            CreateMap<TaskStatusHistory, TaskStatusHistoryDeleteModel>();
            CreateMap<TaskStatusHistoryDeleteModel, TaskStatusHistory>();
        }
       
    }
}
