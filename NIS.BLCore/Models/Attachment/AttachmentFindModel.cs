using System;

namespace NIS.BLCore.Models.Attachment
{
    public class AttachmentFindModel
    {
        public int? Id { get; set; }
        public string FilePath { get; set; }
        public int? CustomerRequestId { get; set; }
        public int? MainTaskId { get; set; }
        public int? CommentId { get; set; }
        public int? FileType { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
