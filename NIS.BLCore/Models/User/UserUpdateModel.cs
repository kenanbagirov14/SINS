namespace NIS.BLCore.Models.User
{
  public class UserUpdateModel
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public string Password { get; set; }
    // public string Salt { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string UserTempId { get; set; }
    public int? AccessFailedCount { get; set; }
    public int? DepartmentId { get; set; }

        public bool IsDeleted { get; set; }
  }
}
