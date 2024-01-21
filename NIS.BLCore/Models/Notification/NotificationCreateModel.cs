using System;
using System.Collections.Generic;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.Tag;

namespace NIS.BLCore.Models.Notification
{
  public class NotificationCreateModel
  {
    public int NotificationTypeId { get; set; }
    public int? UserId { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
  }
}
