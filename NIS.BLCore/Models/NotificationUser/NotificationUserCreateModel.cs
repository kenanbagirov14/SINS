using System;
using System.Collections.Generic;
using System.Web;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.Tag;

namespace NIS.BLCore.Models.NotificationUser
{
    public class NotificationUserCreateModel
    {
        public int UserId { get; set; }
        public bool? Status { get; set; } = false;
        public int? NotificationId { get; set; }
        public string Description { get; set; }
    }
}
