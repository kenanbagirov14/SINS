namespace NIS.BLCore.Models.Rating
{
    public class RatingCreateModel
    {
        public int? MainTaskId { get; set; }
        public int? RequestTaskId { get; set; }
        public int? TaskPoint { get; set; }
        public int? RequestPoint { get; set; }
        public string Description { get; set; }
    }
}
