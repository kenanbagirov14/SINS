using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RequestStatusHistory;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class RequestStatusHistoryController : BaseController
    {
        #region Properties

        private readonly RequestStatusHistoryLogic _requestStatusHistoryLogic;
        private readonly IHubContext<NisHub> _hubContext;

        #endregion

        #region Constructor

        public RequestStatusHistoryController(IConfiguration configuration, IHttpContextAccessor accessor, IHubContext<NisHub> hubContext)
        {
            _hubContext = hubContext;
            _requestStatusHistoryLogic = new RequestStatusHistoryLogic(configuration, accessor.HttpContext.User, hubContext);
        }

        #endregion

        #region RequestStatusHistory Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_requestStatusHistoryLogic.GetAll(filter));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_requestStatusHistoryLogic.GetById(id));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatusHistory Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RequestStatusHistoryFindModel parameter)
        {
            var result = Result(_requestStatusHistoryLogic.Find(parameter));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }


        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RequestStatusHistoryFindModel parameter)
        {
            var result = Result(_requestStatusHistoryLogic.FindAll(parameter));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatusHistory Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RequestStatusHistoryCreateModel entity)
        {
            var result = Result(_requestStatusHistoryLogic.Add(entity));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatusHistory Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RequestStatusHistoryUpdateModel entity)
        {
            var result = Result(_requestStatusHistoryLogic.Update(entity));
            _requestStatusHistoryLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestStatusHistory Remove



        #endregion
    }
}
