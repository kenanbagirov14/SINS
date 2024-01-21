using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Department;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class DepartmentController : BaseController
    {
        #region Properties

        private readonly DepartmentLogic _departmentLogic;

        #endregion

        #region Constructor

        public DepartmentController(IHttpContextAccessor accessor)
        {
            _departmentLogic = new DepartmentLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Department Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_departmentLogic.GetAll(filter));
            _departmentLogic.Dispose();
            return result;

        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_departmentLogic.GetById(id));
            _departmentLogic.Dispose();
            return result;
        }

        #endregion

        #region Department Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]DepartmentFindModel parameter)
        {
            var result = Result(_departmentLogic.Find(parameter));
            _departmentLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]DepartmentFindModel parameter)
        {
            var result = Result(_departmentLogic.FindAll(parameter));
            _departmentLogic.Dispose();
            return result;
        }

        #endregion

        #region Department Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]DepartmentCreateModel entity)
        {
            var result = Result(_departmentLogic.Add(entity));
            _departmentLogic.Dispose();
            return result;
        }

        #endregion

        #region Department Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]DepartmentUpdateModel entity)
        {
            var result = Result(_departmentLogic.Update(entity));
            _departmentLogic.Dispose();
            return result;
        }

        #endregion

        #region Department Remove



        #endregion
    }
}
