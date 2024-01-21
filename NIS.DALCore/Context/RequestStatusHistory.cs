namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("RequestStatusHistory")]
  public partial class RequestStatusHistory
  {
    public int Id { get; set; }

    public int CustomerRequestId { get; set; }

    public int RequestStatusId { get; set; }

    public int UpdatedUserId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedDate { get; set; }

    public string Description { get; set; }

    public virtual CustomerRequest CustomerRequest { get; set; }

    public virtual RequestStatus RequestStatus { get; set; }
    [ForeignKey("UpdatedUserId")]
    public virtual User User { get; set; }
  }
}
