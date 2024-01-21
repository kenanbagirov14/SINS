using System;
using System.Collections.Generic;

namespace NIS.BLCore.Models.Notification
{
  public class NotificationFindModel
  {
    public int? Id { get; set; }
    public int? NotificationTypeId { get; set; }
    public int? UserId { get; set; }

    public string Subject { get; set; }
    public string Content { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    

  }
}
