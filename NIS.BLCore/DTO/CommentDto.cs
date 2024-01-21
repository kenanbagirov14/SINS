using System;

namespace NIS.BLCore.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int MainTaskId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }
    }
}
