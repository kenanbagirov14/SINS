using NIS.BLCore.Extensions;
using System;
using System.Collections.Generic;
using System.Web;

namespace NIS.BLCore.Models.NotificationUser
{
    public class NotificationUserFindModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; } 
        public int? NotificationId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
