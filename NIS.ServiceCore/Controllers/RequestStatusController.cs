using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RequestStatus;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RequestStatusController : BaseController
    {
        #region Properties

        private readonly RequestStatusLogic _requestStatusLogic;

        #endregion

        #region Constructor

        public RequestStatusController(IHttpContextAccessor accessor)
        {
            _requestStatusLogic = new RequestStatusLogic(accessor.HttpContext.User);
        }

        #endregion

        #region RequestStatus Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_requestStatusLogic.GetAll(filter));
            _requestStatusLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_requestStatusLogic.GetById(id));
            _requestStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatus Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RequestStatusFindModel parameter)
        {
            var result = Result(_requestStatusLogic.Find(parameter));
            _requestStatusLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RequestStatusFindModel parameter)
        {
            var result = Result(_requestStatusLogic.FindAll(parameter));
            _requestStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatus Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RequestStatusCreateModel entity)
        {
            var result = Result(_requestStatusLogic.Add(entity));
            _requestStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatus Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RequestStatusUpdateModel entity)
        {
            var result = Result(_requestStatusLogic.Update(entity));
            _requestStatusLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatus Remove



        #endregion
    }
}
