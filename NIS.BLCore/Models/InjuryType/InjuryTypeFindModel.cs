﻿using System;

namespace NIS.BLCore.Models.InjuryType
{
    public class InjuryTypeFindModel
    {
        public int? Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ExecutionDay { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
