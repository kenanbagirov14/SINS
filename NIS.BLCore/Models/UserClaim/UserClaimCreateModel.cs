namespace NIS.BLCore.Models.UserClaim
{
    public class UserClaimCreateModel
    {

        public int? UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

    }
}
