namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserClaim")]
    public partial class UserClaim
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}
