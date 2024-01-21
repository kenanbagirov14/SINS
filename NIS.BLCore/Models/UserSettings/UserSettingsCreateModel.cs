namespace NIS.BLCore.Models.UserSettings
{
  public class UserSettingsCreateModel
  {
    public string Name { get; set; }
    public string Settings { get; set; }
    public int Type { get; set; }
    public string Description { get; set; }
  }
}
