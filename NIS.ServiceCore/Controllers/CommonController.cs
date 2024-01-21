using Newtonsoft.Json;
using NIS.BLCore.Logics;
using NIS.ServiceCore.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using NIS.BLCore.Models.Department;
using NIS.BLCore.Models.User;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.Configuration;
using NIS.BLCore;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : BaseController
    {
        private UserLogic _userLogic;

        private IConfiguration _configuration;
        public CommonController(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _userLogic = new UserLogic(accessor.HttpContext.User);
            _configuration = configuration;
        }

        [HttpGet("Auth")]

        public RedirectResult Auth([FromUri]int authTypeId = 1)
        {

            Task<string> result;
            string url = _configuration["FrontEnd"];
            using (var httpClient = new HttpClient())
            {
                //" + _configuration["Service"] + "common / callback"
                var response = httpClient.PostAsync(new Uri(_configuration["AD"] + "getURL"), new StringContent("projectid=1&authTypeID=" + authTypeId + "& callbackURL=" + _configuration["Service"] + "common/login", Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

                HttpContent content = response.Content;
                result = content.ReadAsStringAsync();
            }
            try
            {
                url = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Result)["url"];
            }
            catch
            {
                // ignored
            }

            return Redirect(url);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm]string data)
        {

            var resp = "";
            data = data.TrimStart('\"').TrimEnd('\"');
            data = data.TrimStart('\'').TrimEnd('\'');
            data = data.Replace('\'', '\"');
            var userData = JsonConvert.DeserializeObject<UserLogin>(data);
            Console.WriteLine("Single sign in CallBack Data: " + data);
            try
            {
                // Check validation of data with presharedKey here
                string preSharedKey = _configuration["PreSharedKey"];
                string check = SHA.SHA128(userData.Username + preSharedKey).ToLower();

                if (check != userData.Secret.ToLower())
                {
                    resp = _configuration["FrontEnd"] + "?login=false";
                    return BadRequest(resp);
                }

                Console.WriteLine("start check user");
                LogicResult<UserViewModel> userExist = new LogicResult<UserViewModel>();

                userExist = _userLogic.Find(new UserFindModel() { UserName = userData.Username, Password = userData.Secret.Substring(0, 6) });
                int? dept = null;
                string depAlias = "";
                string authTypeId = "";
                //Console.WriteLine("user exist", userExist.Output.UserName);

                try
                {
                    string url = _configuration["CommonDBAPI"];

                    Task<string> cdResult;
                    using (var httpClient = new HttpClient())
                    {
                        Console.WriteLine("start demo panel");
                        var resData = httpClient.GetAsync(new Uri(url + "/user/getinfo/" + userData.Username, UriKind.Absolute)).Result;

                        HttpContent content = resData.Content;
                        cdResult = content.ReadAsStringAsync();
                        Console.WriteLine("cdresult", cdResult);

                    }
                    depAlias = JsonConvert.DeserializeObject<CommonDbUser>(cdResult.Result).Alias;
                    authTypeId = JsonConvert.DeserializeObject<CommonDbUser>(cdResult.Result).AuthType;
                }
                catch (Exception)
                {
                    // ignored
                }
                Console.WriteLine("Dep alias: " + depAlias);
                Console.WriteLine("try to find dep from db");
                // Find department with user alias for add to user's DepartmentId
                dept = new DepartmentLogic(User).Find(new DepartmentFindModel() { Alias = depAlias })?.Output.Id;

                Console.WriteLine("dept found from db:{0}", dept);
                if (userExist.Output == null)
                {

                    Console.WriteLine("user output is null, try add new user");
                    // Create a New user
                    var newUser = new UserCreateModel()
                    {
                        UserName = userData.Username,
                        Password = userData.Secret.Substring(0, 6),
                        Email = userData.User.Mail,
                        FirstName = userData.User.FirstName,
                        LastName = userData.User.LastName,
                        DepartmentId = dept
                    };
                    userExist = _userLogic.Add(newUser);
                    Console.WriteLine("new user added");
                }
                Console.WriteLine("user is exist, try update guid");
                //Create new Guid
                var newGuid = new Random().Next().ToString();
                Console.WriteLine("GUID: " + newGuid);
                //Update Current user with new tempId

                _userLogic.Update(new UserUpdateModel
                {
                    UserTempId = userData.Secret.Substring(0, 6) + newGuid + authTypeId,
                    Id = userExist.Output.Id,
                    Email = userData.User.Mail,
                    UserName = userExist.Output.UserName,
                    FirstName = userData.User.FirstName,
                    LastName = userData.User.LastName,
                    MobileNumber = userExist.Output.MobileNumber,
                    PhoneNumber = userExist.Output.PhoneNumber,
                    DepartmentId = dept
                });
                Console.WriteLine("user updatded");
                _userLogic.Dispose();
                //return responce Active Directory ;
                var at = new StringContent(_configuration["Service"] + "common/callBack?tempId=" + newGuid, Encoding.UTF8, "text/plain");
                resp = _configuration["Service"] + "common/callBack?tempId=" + newGuid;
                Console.WriteLine("resp is {0}", _configuration["Service"] + "common/callBack?tempId=" + newGuid);
            }
            catch (Exception ex)
            {
                //return false;
                resp = _configuration["FrontEnd"] + "?login=false&" + userData.Username + "&error=" + ex.Message;
                return BadRequest(resp);
            }
            return Ok(resp);
        }

        [HttpGet("CallBack")]
        public RedirectResult CallBack([FromUri]string tempId)
        {
            var user = _userLogic.Find(new UserFindModel { UserTempId = tempId });
            Console.WriteLine("user " + user.Output.UserName);
            Console.WriteLine("frontent " + _configuration["FrontEnd"]);
            Task<string> result;
            using (var httpClient = new HttpClient())

            {
                var response = httpClient.PostAsync(new Uri(_configuration["ServiceLocal"] + "token", UriKind.Absolute),
                  new StringContent("password=" + user.Output.UserTempId.Substring(0, 6) + "&grant_type=password&username=" + user.Output.UserName,
                  Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

                HttpContent content = response.Content;

                result = content.ReadAsStringAsync();
                Console.WriteLine("restl" + result.Result);
                //}
                _userLogic.Dispose();

                Console.WriteLine("url " + _configuration["FrontEnd"] + "pages/auth/login/?token=" + JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Result)["access_token"]);
                return Redirect(_configuration["FrontEnd"] + "pages/auth/login/?token=" + JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Result)["access_token"]);
            }
        }

        [HttpGet("RetunHashForApi")]
        public ActionResult<string> RetunHashForApi([FromUri]string tempId)
        {
            var user = _userLogic.Find(new UserFindModel { UserTempId = tempId });
            //Console.WriteLine("user " + user.Output.UserName);
            //Console.WriteLine("frontent " + _configuration["FrontEnd"]);
            Task<string> result;
            using (var httpClient = new HttpClient())

            {
                var response = httpClient.PostAsync(new Uri(_configuration["ServiceLocal"] + "token", UriKind.Absolute),
                  new StringContent("password=" + user.Output.UserTempId.Substring(0, 6) + "&grant_type=password&username=" + user.Output.UserName,
                  Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

                HttpContent content = response.Content;

                result = content.ReadAsStringAsync();
                Console.WriteLine("restl" + result.Result);
                //}
                _userLogic.Dispose();

                Console.WriteLine("url " + _configuration["FrontEnd"] + "pages/auth/login/?token=" + JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Result)["access_token"]);
                var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Result)["access_token"];
                return token;
            }
        }
    }
}
