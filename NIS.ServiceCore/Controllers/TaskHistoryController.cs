using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.TaskHistory;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskHistoryController : BaseController
    {
        #region Properties

        private readonly TaskHistoryLogic _taskHistoryLogic;

        #endregion

        #region Constructor

        public TaskHistoryController(IHttpContextAccessor accessor)
        {
            _taskHistoryLogic = new TaskHistoryLogic(accessor.HttpContext.User);
        }

        #endregion

        #region TaskHistory Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_taskHistoryLogic.GetAll(filter));
            _taskHistoryLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_taskHistoryLogic.GetById(id));
            _taskHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskHistory Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]TaskHistoryFindModel parameter)
        {
            var result = Result(_taskHistoryLogic.Find(parameter));
            _taskHistoryLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]TaskHistoryFindModel parameter)
        {
            var result = Result(_taskHistoryLogic.FindAll(parameter));
            _taskHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskHistory Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]TaskHistoryCreateModel entity)
        {
            var result = Result(_taskHistoryLogic.Add(entity));
            _taskHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskHistory Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]TaskHistoryUpdateModel entity)
        {
            var result = Result(_taskHistoryLogic.Update(entity));
            _taskHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region TaskHistory Remove



        #endregion
    }
}
