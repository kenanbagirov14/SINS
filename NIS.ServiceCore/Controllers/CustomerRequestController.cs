using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.DTO;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Mapping;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.SignalR;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.SignalR;
using HybridModelBinding;
using Microsoft.Extensions.Configuration;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CustomerRequestController : BaseController
    {
        #region Properties

        private readonly CustomerRequestLogic _customerRequestLogic;
        private readonly IHubContext<NisHub> _hubContext;
        #endregion

        #region Constructor

        public CustomerRequestController(IHttpContextAccessor accessor, IHubContext<NisHub> hubContext, IConfiguration configuration)
        {
            _customerRequestLogic = new CustomerRequestLogic(accessor.HttpContext.User,hubContext,configuration:configuration);
            _hubContext = hubContext;
        }

        #endregion

        #region Request Get

        // [ClaimsAuthorize("UserId", "9b9be357-9722-448e-890b-0d712c2048ff")]
        //[Route("~/customerRequest/admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_customerRequestLogic.GetAll(filter));
            _customerRequestLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_customerRequestLogic.GetById(id));
            _customerRequestLogic.Dispose();
            return result;
        }

        #endregion

        #region Request Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RequestFindModel parameter)
        {
            var result = Result(_customerRequestLogic.Find(parameter));
            _customerRequestLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RequestFindModel parameter)
        {
           
            var result = Result(_customerRequestLogic.FindAll(parameter));
            _customerRequestLogic.Dispose();
            return result;
        }

        #endregion

        #region Request Add

        [HttpPost("Add")]
        public IActionResult Add([FromHybrid]RequestCreateModel entity)
        {
            var returnedData = _customerRequestLogic.Add(entity);
            var result = Result(returnedData);

            _customerRequestLogic.Dispose();
            return result;


        }

        #endregion

        #region Request Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RequestUpdateModel entity)
        {
            var returnedData = _customerRequestLogic.Update(entity);
            var result = Result(returnedData);
            _customerRequestLogic.Dispose();
            return result;
        }

        #endregion

        #region Request Remove

        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]RequestDeleteModel entity)
        {
            var result = _customerRequestLogic.Remove(entity);
            var resultq= Result(result);
            _customerRequestLogic.Dispose();
            return resultq;
        }

        #endregion
    }
}
