namespace NIS.BLCore.Models.Comment
{
    public class CommentUpdateModel
    {
        public int Id { get; set; }
        public int MainTaskId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
