﻿using System;

namespace NIS.BLCore.DTO
{
    public class TaskHistoryDto
    {
        public int Id { get; set; }
        public int CustomerRequestId { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Description { get; set; }

       // public TaskStatusDto TaskStatus { get; set; }
    }
}
