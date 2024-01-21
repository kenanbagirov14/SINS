using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.Report.Request.CompletedRequest;
using NIS.BLCore.Models.Report.Request.Department;
using NIS.BLCore.Models.Report.Request.Region;
using NIS.BLCore.Models.Report.Request.RequestStatus;
using NIS.BLCore.Models.Report.Request.RequestType;
using NIS.BLCore.Models.Report.Task.Department;
using NIS.BLCore.Models.Report.Task.ExecutorUser;
using NIS.BLCore.Models.Report.Task.TaskStatus;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly ReportLogic _reportLogic;

        public ReportController(IHttpContextAccessor accessor)
        {
            _reportLogic = new ReportLogic(accessor.HttpContext.User);
        }

        #region CustomerRequest

        [HttpPost("RequestByRegion")]
        public RequestRegionReport RequestByRegion([FromBody]RequestFindModel parameters)
        {
            return _reportLogic.RequestByRegion(parameters);
        }

        [HttpPost("RequestByDepartment")]
        public RequestDepartmentReport RequestByDepartment([FromBody]RequestFindModel parameters)
        {
            return _reportLogic.RequestByDepartment(parameters);
        }

        [HttpPost("RequestByRequestType")]
        public RequestRequestTypeReport RequestByRequestType([FromBody]RequestFindModel parameters)
        {
            return _reportLogic.RequestByRequestType(parameters);
        }

        [HttpPost("RequestByRequestStatus")]
        public RequestRequestStatusReport RequestByRequestStatus([FromBody]RequestFindModel parameters)
        {
            return _reportLogic.RequestByRequestStatus(parameters);
        }


        //[HttpPost("RequestByRequestCopmpleted")]
        //public RequstByCompletedReport RequestByRequestCopmpleted([FromBody] RequestFindModel parameters)
        //{
        //    var result = _reportLogic.RequestByCompletedRequest(parameters);

        //    return result;

        //}


        #endregion

        #region MainTask

        [HttpPost("TaskByTaskStatus")]
        public TaskTaskStatusReport TaskByTaskStatus([FromBody]TaskFindModel parameters)
        {
            return _reportLogic.TaskByTaskStatus(parameters);
        }

        [HttpPost("TaskByExecutorUser")]
        public TaskExecutorUserReport TaskByExecutorUser([FromBody]TaskFindModel parameters)
        {
            return _reportLogic.TaskByExecutorUser(parameters);
        }

        [HttpPost("TaskByDepartment")]
        public TaskDepartmentReport TaskByDepartment([FromBody]TaskFindModel parameters)
        {
            return _reportLogic.TaskByDepartment(parameters);
        }

        #endregion
    }
}
