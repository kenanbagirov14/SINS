using System;

namespace NIS.BLCore.Models.Engineer
{
    public class EngineerFindModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
