using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.PhoneNumber
{
  public class PhoneNumberViewModel
  {
    public int Id { get; set; }
    public int? SubscriberId { get; set; }
    public string Number { get; set; }
    public string ProviderName { get; set; }
    public string ADSL { get; set; }
    public string Description { get; set; }
    public string CaseNumber { get; set; }
    public string CableBox { get; set; }
    public string Strip { get; set; }
    public string StripPair { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual SubscriberDto Subscriber { get; set; }
  }
}
