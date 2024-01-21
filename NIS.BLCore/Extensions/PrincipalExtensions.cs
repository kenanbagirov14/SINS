using Newtonsoft.Json;
using NIS.BLCore.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace NIS.BLCore.Extensions
{
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Returns authenticated user id.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static int GetUserId(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;
            return int.Parse(claimsPrincipal != null && claimsPrincipal.HasClaim(s => s.Type.Equals("UserId"))
                ? claimsPrincipal.Claims.First(c => c.Type.Equals("UserId")).Value
                : "0");
        }
        public static List<string> GetUserRoles(this IPrincipal principal)
        {

            return principal is ClaimsPrincipal claimsPrincipal && claimsPrincipal.HasClaim(s => s.Type.Equals("Roles"))
                ? JsonConvert.DeserializeObject<List<string>>(claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals("Roles"))?.Value)
                : new List<string>();
        }
        public static List<string> GetUserDepartments(this IPrincipal principal)
        {
            return principal is ClaimsPrincipal claimsPrincipal && claimsPrincipal.HasClaim(s => s.Type.Equals("Department"))
                ? JsonConvert.DeserializeObject<List<string>>(claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals("Department"))?.Value)
                : new List<string>();
        }

        public static List<UserVirtualDepartment> GetUserVirtualDepartments(this IPrincipal principal)
        {
            return principal is ClaimsPrincipal claimsPrincipal && claimsPrincipal.HasClaim(d => d.Type.Equals("VirtualDepartment"))
              ? JsonConvert.DeserializeObject<List<UserVirtualDepartment>>
                (claimsPrincipal.Claims.FirstOrDefault(x => x.Type.Equals("VirtualDepartment"))?.Value)
              : new List<UserVirtualDepartment>();
        }
    }
}
