using System;

namespace NIS.BLCore.Models.PhoneNumber
{
  public class PhoneNumberFindModel
  {
    public int? Id { get; set; }
    public int? SubscriberId { get; set; }
    public string Number { get; set; }
    public string ProviderName { get; set; }
    public string ADSL { get; set; }
    public string CaseNumber { get; set; }
    public string CableBox { get; set; }
    public string Strip { get; set; }
    public string StripPair { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
