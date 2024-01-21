namespace NIS.BLCore.Models.Rating
{
    public class RatingUpdateModel
    {
        public int Id { get; set; }
        public int? MainTaskId { get; set; }
        public int? RequestId { get; set; }
        public int? TaskPoint { get; set; }
        public int? RequestPoint { get; set; }
        public string Description { get; set; }
    }
}
