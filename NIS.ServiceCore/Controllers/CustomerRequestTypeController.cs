using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.CustomerRequestType;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerRequestTypeController : BaseController
    {
        #region Properties

        private readonly CustomerRequestTypeLogic _customerRequestTypeLogic;

        #endregion

        #region Constructor

        public CustomerRequestTypeController(IHttpContextAccessor accessor)
        {
            _customerRequestTypeLogic = new CustomerRequestTypeLogic(accessor.HttpContext.User);
        }

        #endregion

        #region RequestType Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_customerRequestTypeLogic.GetAll(filter));
            _customerRequestTypeLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_customerRequestTypeLogic.GetById(id));
            _customerRequestTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestType Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RequestTypeFindModel parameter)
        {
            var result = Result(_customerRequestTypeLogic.Find(parameter));
            _customerRequestTypeLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RequestTypeFindModel parameter)
        {
            var result = Result(_customerRequestTypeLogic.FindAll(parameter));
            _customerRequestTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestType Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RequestTypeCreateModel entity)
        {
            var result = Result(_customerRequestTypeLogic.Add(entity));
            _customerRequestTypeLogic.Dispose();
            return result;
        }


        #endregion

        #region RequestType Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RequestTypeUpdateModel entity)
        {
            var result = Result(_customerRequestTypeLogic.Update(entity));
            _customerRequestTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region RequestType Remove



        #endregion
    }
}
