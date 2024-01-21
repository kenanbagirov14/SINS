namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("NotificationUser")]
  public partial class NotificationUser
  {
    public int Id { get; set; }

    public int UserId { get; set; }
    public int? NotificationId { get; set; }
    public bool Status { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }

    public virtual Notification Notification { get; set; }
    public virtual User User { get; set; }
  }
}
