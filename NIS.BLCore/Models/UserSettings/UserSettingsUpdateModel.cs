
namespace NIS.BLCore.Models.UserSettings
{
  public class UserSettingsUpdateModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Settings { get; set; }
    public int Type { get; set; }
    public string Description { get; set; }
  }
}
