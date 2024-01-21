using System.Collections.Generic;
using NIS.BLCore.Models.Attachment;

namespace NIS.BLCore.Models.Comment
{
    public class CommentCreateModel
    {
        public int MainTaskId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public List<AttachmentCreateModel> Attachment { get; set; }
    }
}
