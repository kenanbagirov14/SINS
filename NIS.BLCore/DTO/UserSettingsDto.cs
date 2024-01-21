using System;

namespace NIS.BLCore.DTO
{
  public class UserSettingsDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Settings { get; set; }
    public int Type { get; set; }
    public int UserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
  }
}
