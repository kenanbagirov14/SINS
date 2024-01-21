using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Notification
{
    public class NotificationViewModel
    {
    public int Id { get; set; }

    public int NotificationTypeId { get; set; }
    public int? UserId { get; set; }

    public string Subject { get; set; }
    public string Content { get; set; }
     
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }
    public NotificationTypeDto NotificationType { get; set; }
    public UserDto User { get; set; }
  }
}
