using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Project;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateModel, Project>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Project, ProjectDto>();
            CreateMap<Project, ProjectCreateModel>();
            CreateMap<ProjectCreateModel, Project>();
            CreateMap<Project, ProjectUpdateModel>();
            CreateMap<ProjectUpdateModel, Project>();
            CreateMap<Project, ProjectDeleteModel>();
            CreateMap<ProjectDeleteModel, Project>();
        }
       
    }
}
