using System;

namespace NIS.BLCore.DTO
{
    public class SubscriberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Organisation { get; set; }
        public string PassportNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
