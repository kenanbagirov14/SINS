using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Project;
using NIS.BLCore.Models.Core;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using NIS.UtilsCore;
using NIS.UtilsCore.Enums;
using System.Linq;
using System.Web;
using NIS.BLCore;
using NIS.BLCore.Extensions;
using System;
using Microsoft.AspNetCore.Mvc;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.Configuration;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : BaseController
    {
        #region Properties

        private readonly ProjectLogic _projectLogic;
        private IConfiguration _configuration;

        #endregion

        #region Constructor

        public ProjectController(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _projectLogic = new ProjectLogic(accessor.HttpContext.User);
            _configuration = configuration;
        }

        #endregion

        #region Project Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            //Task<string> cdResult = null;
            //string url = _configuration["CommonDBAPI"];
            var result = new BLCore.LogicResult<ICollection<ProjectViewModel>>();
            //using (var httpClient = new HttpClient())
            //{
            //  var data = httpClient.GetAsync(new System.Uri(url + "/projects", System.UriKind.Absolute)).Result;
            //  HttpContent content = data.Content;
            //  cdResult = content.ReadAsStringAsync();

            //}
            try
            {
                var virtualDepartments = JsonConvert.SerializeObject(User.GetUserVirtualDepartments().Select(x => new ProjectViewModel() { Name = x.Alias, Description = JsonConvert.SerializeObject(x.UserNames) }));
                result.Output = JsonConvert.DeserializeObject<List<ProjectViewModel>>(virtualDepartments).ToList();

            }
            catch (Exception ex)
            {

                result.ErrorList.Add(new Error()
                {
                    Type = OperationResultCode.Exception,
                    Code = "SystemError",
                    Text = ex.Message
                });
            }
            // return  Result(_projectLogic.GetAll(filter));
            return Result(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_projectLogic.GetById(id));
        }

        #endregion

        #region Project Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]ProjectFindModel parameter)
        {
            return Result(_projectLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]ProjectFindModel parameter)
        {
            return Result(_projectLogic.FindAll(parameter));
        }

        #endregion

        #region Project Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]ProjectCreateModel entity)
        {
            var result = new LogicResult<ProjectViewModel>();
            //string url = _configuration["CommonDBAPI"];
            //Task<string> cdResult = null;
            // result.Output.UserName = "admin";
            //try
            //{
            //    using (var httpClient = new HttpClient())
            //    {
            //        var json = new { project_name = entity.Name, auth_types = new int[] { 1 } };
            //        var data = httpClient.PostAsJsonAsync(new System.Uri(url + "/projects/create",
            //            System.UriKind.Absolute),
            //            json).Result;
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
            //    result.Output = JsonConvert.DeserializeObject<ProjectViewModel>(cdResult.Result);

            //}
            //catch
            //{

            //    result.ErrorList.Add(new Error()
            //    {
            //        Type = OperationResultCode.Exception,
            //        Code = "SystemError",
            //        Text = cdResult.Result
            //    });
            //}
            //Task.Run(() =>
            //{
            //    _projectLogic.Add(entity);
            //});
            //return Result(result);
            return Result(_projectLogic.Add(entity));
        }

        #endregion

        #region Project Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]ProjectUpdateModel entity)
        {
            return Result(_projectLogic.Update(entity));
        }

        #endregion

        #region Project Remove



        #endregion
    }
}
