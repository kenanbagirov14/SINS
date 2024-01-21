using System.Threading.Tasks;
using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.MainTask;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class ArProfile:Profile
    {
        public ArProfile()
        {
            CreateMap<TaskCreateModel, MainTask>();
            CreateMap<MainTask, TaskViewModel>();
            CreateMap<Task<MainTask>, Task<TaskViewModel>>();
            CreateMap<MainTask, TaskDto>();
            CreateMap<MainTask, TaskCreateModel>();
            CreateMap<TaskCreateModel, MainTask>();
            CreateMap<MainTask, TaskUpdateModel>();
            CreateMap<TaskUpdateModel, MainTask>();
            CreateMap<MainTask, TaskDeleteModel>();
            CreateMap<TaskDeleteModel, MainTask>();
        }
       
    }
}
