﻿using System;

namespace NIS.BLCore.Models.UserSettings
{
  public class UserSettingsFindModel
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Settings { get; set; }
    public int? Type { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public int? UserId { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}