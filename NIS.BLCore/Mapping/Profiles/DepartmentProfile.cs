using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Department;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentCreateModel, Department>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, ChildDepartmentDto>();
            CreateMap<DepartmentDto, ChildDepartmentDto>();
            CreateMap<Department, DepartmentCreateModel>();
            CreateMap<DepartmentCreateModel, Department>();
            CreateMap<Department, DepartmentUpdateModel>();
            CreateMap<DepartmentUpdateModel, Department>();
            CreateMap<Department, DepartmentDeleteModel>();
            CreateMap<DepartmentDeleteModel, Department>();
        }
       
    }
}
