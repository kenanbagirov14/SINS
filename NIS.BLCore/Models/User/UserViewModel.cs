using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int? AccessFailedCount { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string ConnectedId { get; set; }
        public int? DepartmentId { get; set; }
        public string UserTempId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual UserDto ParentUser { get; set; }
        public virtual DepartmentDto Department { get; set; }
        //public virtual ICollection<CommentDto> Comment { get; set; }
        public virtual ICollection<UserClaimDto> UserClaim { get; set; }
        public virtual ICollection<RoleDto> Role { get; set; }
        public virtual ICollection<UserDto> ChildUsers { get; set; }


        public virtual ICollection<string> Roles { get; set; } //Todo -- set new class
        public virtual ICollection<string> Departments { get; set; }
        public virtual ICollection<string> VirtualDepartments { get; set; }

    }

    public class UserData
    {
        public ICollection<Roles> Functions { get; set; }
        public ICollection<Structure> Structures { get; set; }
    }

    public class Roles
    {
        public string Function { get; set; }
        public string Function_Name { get; set; }
    }
    public class Structure
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public bool Virtual { get; set; } = false;
        public List<string> User_names { get; set; }
    }
    public class CommonDbUser
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string Alias { get; set; }
        public string AuthType { get; set; }
    }

    public class InputData
    {
        public string Data { get; set; }
    }

    public class UserLogin
    {
        public string Secret { get; set; }
        public string Username { get; set; }
        public UserDetails User { get; set; }

    }
    public class UserDetails
    {
        public string Mail { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class UserVirtualDepartment
    {
        public string Alias { get; set; }
        public List<string> UserNames { get; set; }
    }
}
