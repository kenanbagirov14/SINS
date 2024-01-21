using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NIS.BLCore.Extensions;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using NIS.BLCore.Models.SignalR;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class MainTaskController : BaseController
    {
        #region Properties

        private readonly MainTaskLogic _taskLogic;
        private readonly CustomerRequestLogic _requsetLogic;
        private readonly RequestStatusHistoryLogic _requestHistoryLogic;
        private readonly IHubContext<NisHub> _hubContext;
        #endregion

        #region Constructor

        public MainTaskController(IConfiguration configuration, IHttpContextAccessor accessor, IHubContext<NisHub> hubContext)
        {
            _hubContext = hubContext;
            _taskLogic = new MainTaskLogic(configuration,accessor.HttpContext.User,  hubContext);
            _requsetLogic = new CustomerRequestLogic(accessor.HttpContext.User, hubContext,configuration:configuration);
            _requestHistoryLogic = new RequestStatusHistoryLogic(configuration,accessor.HttpContext.User,  hubContext);
        }

        #endregion

        #region Task Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_taskLogic.GetAll(filter));
            _taskLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_taskLogic.GetById(id));
            _taskLogic.Dispose();
            return result;
        }

        #endregion

        #region Task Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]TaskFindModel parameter)
        {
            var result = Result(_taskLogic.Find(parameter));
            _taskLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]TaskFindModel parameter)
        {
            var result = Result(_taskLogic.FindAll(parameter));
            _taskLogic.Dispose();
            return result;
        }

        #endregion

        #region Task Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]TaskCreateModel entity)
        {
            var returnedData = _taskLogic.Add(entity);
            var result = Result(returnedData);
            _taskLogic.Dispose();
            return result;
        }

        #endregion

        #region Task Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]TaskUpdateModel entity)
        {
            var returnedData = _taskLogic.Update(entity);

            SignalrReturneddata<TaskViewModel> data = new SignalrReturneddata<TaskViewModel>()
            {
                NewData = returnedData.Output
            };
            var signalrAddeddata = JObject.Parse(JsonConvert.SerializeObject(data,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            _hubContext.Clients.All.SendAsync("remoteTaskUpdate",signalrAddeddata);
            var result = Result(returnedData);
            _taskLogic.Dispose();
            return result;
        }

        #endregion

        #region Task Remove

        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]TaskDeleteModel entity)
        {
            var result = _taskLogic.Remove(entity);
            
            var aresult = Result(result);
            _taskLogic.Dispose();
            return aresult;
        }

        #endregion
    }
}
