using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.TaskHistory;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class TaskHistoryProfile : Profile
    {
        public TaskHistoryProfile()
        {
            CreateMap<TaskHistoryCreateModel, TaskHistory>();
            CreateMap<TaskHistory, TaskHistoryViewModel>();
            CreateMap<TaskHistory, TaskHistoryDto>();
            CreateMap<TaskHistory, TaskHistoryCreateModel>();
            CreateMap<TaskHistoryCreateModel, TaskHistory>(); 
            CreateMap<TaskHistory, TaskHistoryUpdateModel>();
            CreateMap<TaskHistoryUpdateModel, TaskHistory>();
            CreateMap<TaskHistory, TaskHistoryDeleteModel>();
            CreateMap<TaskHistoryDeleteModel, TaskHistory>();
        }
       
    }
}
