namespace NIS.DALCore.Context
{
  using System;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("PhoneNumber")]
  public partial class PhoneNumber
  {
    public int Id { get; set; }

    public int? SubscriberId { get; set; }

    public string Number { get; set; }

    public string CaseNumber { get; set; }
    public string CableBox { get; set; }
    public string Strip { get; set; }
    public string StripPair { get; set; }

    public string ProviderName { get; set; }

    public string ADSL { get; set; }

    public string Description { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public virtual Subscriber Subscriber { get; set; }
  }
}
