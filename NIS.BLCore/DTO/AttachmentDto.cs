﻿using System;

namespace NIS.BLCore.DTO
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int? CustomerRequestId { get; set; }
        public int? MainTaskId { get; set; }
        public int? CommentId { get; set; }
        public int? FileType { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
