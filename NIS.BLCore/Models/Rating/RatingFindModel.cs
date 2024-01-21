using System;

namespace NIS.BLCore.Models.Rating
{
    public class RatingFindModel
    {
        public int? Id { get; set; }
        public int? MainTaskId { get; set; }      
        public int? RequestId { get; set; }      
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

    }
}
