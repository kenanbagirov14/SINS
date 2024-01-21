using System;

namespace NIS.BLCore.DTO
{
    public class NotificationDto
    {
    public int Id { get; set; }

    public int NotificationTypeId { get; set; }
    public int? UserId { get; set; }

    public string Subject { get; set; }
    public string Content { get; set; }
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }
    public UserDto User { get; set; }
    public NotificationTypeDto NotificationType { get; set; }

  }
}
