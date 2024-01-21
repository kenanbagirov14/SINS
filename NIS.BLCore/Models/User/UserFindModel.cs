using System;

namespace NIS.BLCore.Models.User
{
  public class UserFindModel
  {
    public int? Id { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public int? AccessFailedCount { get; set; }
    public string ConnectedId { get; set; }
    public string UserTempId { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
