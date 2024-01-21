using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;
using NIS.DALCore.Model;

namespace NIS.BLCore.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int MainTaskId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }


        public virtual TaskDto MainTask { get; set; }
        public virtual UserDto User { get; set; }

        public List<AttachmentDto> Attachment { get; set; }
    }
}
