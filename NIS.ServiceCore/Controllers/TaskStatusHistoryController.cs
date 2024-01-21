using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskStatusHistory;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskStatusHistoryController : BaseController
    {
        #region Properties

        private readonly TaskStatusHistoryLogic _taskStatusHistoryLogic;

        #endregion

        #region Constructor

        public TaskStatusHistoryController(IConfiguration configuration,IHttpContextAccessor accessor)
        {
            _taskStatusHistoryLogic = new TaskStatusHistoryLogic(configuration, accessor.HttpContext.User);
        }

        #endregion

        #region TaskStatusHistory Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_taskStatusHistoryLogic.GetAll(filter));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_taskStatusHistoryLogic.GetById(id));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatusHistory Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]TaskStatusHistoryFindModel parameter)
        {
            var result = Result(_taskStatusHistoryLogic.Find(parameter));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]TaskStatusHistoryFindModel parameter)
        {
            var result = Result(_taskStatusHistoryLogic.FindAll(parameter));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatusHistory Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]TaskStatusHistoryCreateModel entity)
        {
            var result = Result(_taskStatusHistoryLogic.Add(entity));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatusHistory Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]TaskStatusHistoryUpdateModel entity)
        {
            var result = Result(_taskStatusHistoryLogic.Update(entity));
            _taskStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskStatusHistory Remove



        #endregion
    }
}
