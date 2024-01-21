using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using log4net;
using Newtonsoft.Json;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.User;
using NIS.BLCore.Models.UserClaim;
using NIS.DALCore.Context;
using NIS.UtilsCore.Enums;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Models.Core;

namespace NIS.Security
{
    public class JWTToken
    {
        private readonly UserLogic _userLogic;
        private UserClaimLogic _userClaimLogic;
        private NISContext _context;
        private IConfiguration _configuration;
        private ClaimsPrincipal _user;

        public JWTToken(IConfiguration Configuration, ClaimsPrincipal user)
        {
            _context = new NISContext();
            _userLogic = new UserLogic(user);
            _userClaimLogic = new UserClaimLogic(user);
            _configuration = Configuration;
        }

        public JWTResult SingIn(UserLoginData context)
        {
            JWTResult result = new JWTResult();
            // create a logger 
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //Validate user
            User user = _userLogic.ValidateUser(context.UserName, context.Password);

            // check Vlidated user if is not exist return "invalid_grant" error
            if (user == null)
            {
                result.errorMessage = "invalid_grant" + ":" + "The user name or password is incorrect.";
                result.error = true;
                return result;
            }

            //get authType from userTempId (last character of usertempid)
            var authTypeId = user.UserTempId.Substring(user.UserTempId.Length - 1, 1);

            //Comondb url from appsettings
            string url = _configuration["CommonDBAPI"];

            Task<string> cdResult = null;
            try
            {

                //Get user roles and departments from comondb
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var at = httpClient.PostAsync(new System.Uri(url + "/functionbyusername", System.UriKind.Absolute),
                      new System.Net.Http.StringContent("projectid=1&authtypeid=" + authTypeId + "&username=" + user.UserName + "&at=" + System.DateTime.Now,
                      System.Text.Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                    System.Net.Http.HttpContent content = at.Content;
                    cdResult = content.ReadAsStringAsync();
                }
            }
            catch (System.Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                logger.Info(
                   "getuserdata from common: AuthType: " + authTypeId + " -- username: " + user.UserName
                  + $"{Environment.NewLine}"
                  + " - Executed class : " + this.GetType()
                  + $"{Environment.NewLine}"
                  + " - Executed method : Find");
            }

            List<string> roles = new List<string>();
            List<string> departments = new List<string>();
            List<UserVirtualDepartment> virtualDepartment = new List<UserVirtualDepartment>();
            try
            {
                //check  cdResult 
                logger.Info("cdResult: " + cdResult?.Result);
                if (cdResult != null)
                {
                    var userData = JsonConvert.DeserializeObject<UserData>(cdResult.Result);
                    roles = userData.Functions.Where(x => !x.Function_Name.ToLower().Contains("s_")).Select(x => x.Function).ToList();
                    departments = userData.Structures.Where(s => !s.Virtual).Select(x => x.Alias).ToList();
                    virtualDepartment = userData.Structures.Where(s => s.Virtual).Select(x => new UserVirtualDepartment() { Alias = x.Alias, UserNames = x.User_names }).ToList();

                    Task.Run(() =>
                    {
                        SetUserClaims(_user,user.Id, departments);
                    });
                }
            }
            catch
            {
                // ignored
            }

            // Get valid claims and pass them into JWT
            var identity = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Roles", JsonConvert.SerializeObject(roles)),
                new Claim("Department", JsonConvert.SerializeObject(departments)),
                new Claim("VirtualDepartment", JsonConvert.SerializeObject(virtualDepartment)),
                new Claim(ClaimTypes.UserData, "data")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretmecretsecret"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                //issuer: _jwtOptions.Issuer,
                //audience: _jwtOptions.Audience,
                claims: identity,
                //notBefore: _jwtOptions.NotBefore,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JWTResult()
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(jwt),
                Department = JsonConvert.SerializeObject(departments),
                Roles = JsonConvert.SerializeObject(roles),
                UserId = user.Id,
                UserName = user.UserName,
                error = false
            };

        }

        /// <summary>
        /// Set user Claims to db
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departments"></param>
        private void SetUserClaims(ClaimsPrincipal user ,int userId, List<string> departments)
        {
            _userClaimLogic = new UserClaimLogic(user);
            var userclaim = _userClaimLogic.FindAll(new UserClaimFindModel
            {
                UserId = userId,
                ClaimType = ClaimTypeEnum.Department.ToString()
            });

            var userclaimdep = userclaim.Output.Select(s => s.ClaimValue).ToList();
            var haveToRemove = userclaimdep.Except(departments).ToList();
            var excepteddep = departments.Except(userclaimdep).ToList();
            var newClaims = new List<UserClaimCreateModel>();
            var deletedlist = userclaim.Output.Where(u => haveToRemove.Contains(u.ClaimValue)).ToList();
            excepteddep.ForEach(e => newClaims.Add(new UserClaimCreateModel
            {
                ClaimType = ClaimTypeEnum.Department.ToString(),
                ClaimValue = e,
                UserId = userId

            }));
            if (newClaims.Count > 0)
            {
            _userClaimLogic = new UserClaimLogic(user);
                _userClaimLogic.AddRange(newClaims);
            }
            if (deletedlist.Count > 0)
            {
            _userClaimLogic = new UserClaimLogic(user);
                _userClaimLogic.RemoveRange(deletedlist);
            }

            // dispose context
            _userLogic.Dispose();
        }
    }
}
