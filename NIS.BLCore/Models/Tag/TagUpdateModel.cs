namespace NIS.BLCore.Models.Tag
{
    public class TagUpdateModel
    {
        public int Id { get; set; }
        public int? MainTaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
