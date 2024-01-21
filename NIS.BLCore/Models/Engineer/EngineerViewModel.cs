using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Engineer
{
    public class EngineerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalCount { get; set; }

        public  List<DepartmentDto> Department { get; set; }
        public  List<TaskDto> MainTask { get; set; }
    }
}
