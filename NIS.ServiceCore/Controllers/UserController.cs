using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.User;
using NIS.BLCore.Extensions;
using NIS.BLCore.Models.Core;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseController
    {
        #region Properties

        private readonly UserLogic _userLogic;

        #endregion

        #region Constructor

        private IConfiguration _configuration;


        public UserController(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _userLogic = new UserLogic(accessor.HttpContext.User);
            _configuration = configuration;
        }

        #endregion

        #region User Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_userLogic.GetAll(filter));
            _userLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_userLogic.GetById(id));
            _userLogic.Dispose();
            return result;
        }

        #endregion

        #region User Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]UserFindModel parameter)
        {
            var result = Result(_userLogic.Find(parameter));
            _userLogic.Dispose();
            return result;
        }

        [HttpPost("FindByToken")]
        public IActionResult FindByToken()
        {
            var result = _userLogic.Find(new UserFindModel() { UserName = User.Identity.Name });
            //string url = _configuration["CommonDBAPI"];
           // Task<string> cdResult = null;
            // result.Output.UserName = "admin";

            //var authTypeId = result.Output.UserTempId.Substring(result.Output.UserTempId.Length - 1, 1);
            //try
            //{
            //    using (var httpClient = new HttpClient())
            //    {
            //        var data = httpClient.PostAsync(new System.Uri(url + "/functionbyusername", System.UriKind.Absolute), new StringContent("projectid=1&authtypeid=" + authTypeId + "&username=" + result.Output.UserName, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
            //        HttpContent content = data.Content;
            //        cdResult = content.ReadAsStringAsync();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    while (ex.InnerException != null)
            //        ex = ex.InnerException;

            //    result.ErrorList.Add(new Error()
            //    {
            //        Type = OperationResultCode.Exception,
            //        Code = "SystemError",
            //        Text = ex.Message
            //    });
            //}
            //try
            //{
            //    if (cdResult != null)
            //    {
            //        var userData = JsonConvert.DeserializeObject<UserData>(cdResult.Result);
            //        result.Output.Roles = userData.Functions.Where(x => !x.Function_Name.ToLower().Contains("s_")).Select(x => x.Function).ToList();
            //        result.Output.Departments = userData.Structures.Where(s => !s.Virtual).Select(x => x.Alias).ToList();
            //        result.Output.VirtualDepartments = userData.Structures.Where(s => s.Virtual).Select(x => x.Alias).ToList();
            //    }
            //}
            //catch (System.Exception excc)
            //{
            //    while (excc.InnerException != null)
            //        excc = excc.InnerException;
            //    result.ErrorList.Add(new Error()
            //    {
            //        Type = OperationResultCode.Exception,
            //        Code = "SystemError",
            //        Text = excc.Message
            //    });
            //}
            result.Output.Roles = User.GetUserRoles();
            result.Output.Departments = User.GetUserDepartments();
            result.Output.VirtualDepartments = User.GetUserVirtualDepartments().Select(x => x.Alias).ToList();
            var resulta = Result(result);
            _userLogic.Dispose();
            return resulta;
        }


        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]UserFindModel parameter)
        {
            var result = Result(_userLogic.FindAll(parameter));
            _userLogic.Dispose();
            return result;
        }

        #endregion

        #region User Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]UserCreateModel entity)
        {
            var result = Result(_userLogic.Add(entity));
            _userLogic.Dispose();
            return result;
        }

        #endregion

        #region User Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]UserUpdateModel entity)
        {
            var result = Result(_userLogic.Update(entity));
            _userLogic.Dispose();
            return result;
        }

        #endregion

        #region User Remove

        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]UserDeleteModel entity)
        {
            var result = Result(_userLogic.Remove(entity));
            _userLogic.Dispose();
            return result;
        }

        #endregion
    }
}
