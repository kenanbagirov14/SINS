namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserRoles
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
