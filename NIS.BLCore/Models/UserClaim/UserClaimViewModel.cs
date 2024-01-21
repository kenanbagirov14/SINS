using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.UserClaim
{
    public class UserClaimViewModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual UserDto User { get; set; }
    }
}
