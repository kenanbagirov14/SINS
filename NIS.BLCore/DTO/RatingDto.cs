using System;

namespace NIS.BLCore.DTO
{
    public class RatingDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? MainTaskId { get; set; }
        public int? RequestId { get; set; }
        public int? RequestPoint { get; set; }
        public int? TaskPoint { get; set; }
    }
}
