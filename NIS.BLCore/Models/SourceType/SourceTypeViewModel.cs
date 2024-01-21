﻿using System;

namespace NIS.BLCore.Models.SourceType
{
    public class SourceTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        // public virtual List<RequestStatusHistoryDto> RequestStatusHistory { get; set; }
    }
}