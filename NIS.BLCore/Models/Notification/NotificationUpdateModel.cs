using System;
using System.Collections.Generic;
using NIS.BLCore.Models.Tag;
using NIS.BLCore.Models.Rating;
namespace NIS.BLCore.Models.Notification
{
    public class NotificationUpdateModel
    {
    public int Id { get; set; }

    public int NotificationTypeId { get; set; }
    public int? UserId { get; set; }

    public string Subject { get; set; }
    public string Content { get; set; }

    public string Description { get; set; }
  }
}
