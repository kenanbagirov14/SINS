using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskStatus;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskStatusController : BaseController
    {
        #region Properties

        private readonly TaskStatusLogic _taskStatusLogic;

        #endregion

        #region Constructor

        public TaskStatusController(IHttpContextAccessor accessor)
        {
            _taskStatusLogic = new TaskStatusLogic(accessor.HttpContext.User);
        }

        #endregion

        #region TaskStatus Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_taskStatusLogic.GetAll(filter));
            _taskStatusLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_taskStatusLogic.GetById(id));
            _taskStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatus Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]TaskStatusFindModel parameter)
        {
            var result = Result(_taskStatusLogic.Find(parameter));
            _taskStatusLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]TaskStatusFindModel parameter)
        {
            var result = Result(_taskStatusLogic.FindAll(parameter));
            _taskStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatus Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]TaskStatusCreateModel entity)
        {
            var result = Result(_taskStatusLogic.Add(entity));
            _taskStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatus Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]TaskStatusUpdateModel entity)
        {
            var result = Result(_taskStatusLogic.Update(entity));
            _taskStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatus Remove



        #endregion
    }
}
