using AutoMapper;
using NIS.BL.DTO;
using NIS.BL.Models.Department;
using NIS.DAL.Context;

namespace NIS.BL.Mapping.Core
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentCreateModel, Department>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentCreateModel>();
            CreateMap<DepartmentCreateModel, Department>();
            CreateMap<Department, DepartmentUpdateModel>();
            CreateMap<DepartmentUpdateModel, Department>();
            CreateMap<Department, DepartmentDeleteModel>();
            CreateMap<DepartmentDeleteModel, Department>();
        }
       
    }
}
