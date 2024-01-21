using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.NotificationUser
{
    public class NotificationUserViewModel
    {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? NotificationId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool Status { get; set; }
    public string Description { get; set; }
    public  NotificationDto Notification { get; set; }
    //public  UserDto User { get; set; }
  }
}
