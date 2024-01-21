using System;

namespace NIS.BLCore.DTO
{
    public class NotificationUserDto
    {
    public int Id { get; set; }

    public int UserId { get; set; }
    public int? NotificationId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
  }
}
