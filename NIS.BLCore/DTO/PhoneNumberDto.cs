using System;

namespace NIS.BLCore.DTO
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }

        public int? SubscriberId { get; set; }

        public string Number { get; set; }

        public string ProviderName { get; set; }

        public string ADSL { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
