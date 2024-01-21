using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.TaskStatusHistory;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
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
