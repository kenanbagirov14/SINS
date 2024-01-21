using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Rating
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int? MainTaskId { get; set; }
        public int? RequestId { get; set; }
        public int? RequestPoint { get; set; }
        public int? TaskPoint { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public TaskDto MainTask { get; set; }
        public RequestDto CustomerRequest { get; set; }
    }
}
