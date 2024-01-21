﻿using System;

namespace NIS.BLCore.Models.WorkFlow
{
    public class WorkFlowUpdateModel
    {
        public int Id { get; set; }

        public int? CustomerRequestId { get; set; }

        public int? DepartmentId { get; set; }

        public int? Type { get; set; }

        public string ConditionKey { get; set; }

        public string ConditionValue { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
