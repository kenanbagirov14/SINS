namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  [Table("Notification")]
  public partial class Notification
  {
    public int Id { get; set; }
    public int NotificationTypeId { get; set; }
    public int? UserId { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }

    public virtual User User { get; set; }
    public virtual NotificationType NotificationType { get; set; }

  }
}
