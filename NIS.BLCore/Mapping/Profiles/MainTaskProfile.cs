using System.Threading.Tasks;
using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.MainTask;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class ArProfile:Profile
    {
        public ArProfile()
        {
            CreateMap<TaskCreateModel, MainTask>();
            CreateMap<MainTask, TaskViewModel>();
            CreateMap<Task<MainTask>, Task<TaskViewModel>>();
            CreateMap<TaskDto,MainTask>();
            CreateMap<MainTask, TaskDto>();
            CreateMap<MainTask, TaskCreateModel>();
            CreateMap<TaskCreateModel, MainTask>();
            CreateMap<MainTask, TaskUpdateModel>();
            CreateMap<TaskUpdateModel, MainTask>();
            CreateMap<MainTask, TaskDeleteModel>();
            CreateMap<TaskDeleteModel, MainTask>();
            CreateMap<TaskCreateModel, TaskViewModel>();
        }
       
    }
}
