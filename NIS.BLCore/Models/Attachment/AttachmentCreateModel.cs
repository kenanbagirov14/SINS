namespace NIS.BLCore.Models.Attachment
{
    public class AttachmentCreateModel
    {
        public string FilePath { get; set; }
        public int? CustomerRequestId { get; set; }
        public int? MainTaskId { get; set; }
        public int? CommentId { get; set; }
        public int? FileType { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; }
        public string Container { get; set; }
        public string Extension { get; set; }
    }
}
