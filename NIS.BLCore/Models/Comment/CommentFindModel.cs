using System;

namespace NIS.BLCore.Models.Comment
{
    public class CommentFindModel
    {
        public int? Id { get; set; }
        public int? MainTaskId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
