using System;

namespace NIS.BLCore.Models.UserClaim
{
    public class UserClaimFindModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

    }
}
