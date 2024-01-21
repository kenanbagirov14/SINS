using System;
using System.Collections.Generic;
using NIS.BLCore.Models.Tag;
using NIS.BLCore.Models.Rating;
using System.Web;
using NIS.BLCore.Extensions;

namespace NIS.BLCore.Models.NotificationUser
{
  public class NotificationUserUpdateRangeModel
  {
    public List<int> Id { get; set; }
    public bool Status { get; set; }
    public string Description { get; set; }
  }
}
