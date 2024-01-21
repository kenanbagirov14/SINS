using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.RealTimeConnection;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RealTimeConnectionController : BaseController
    {
        #region Properties

        private readonly RealTimeConnectionLogic _realTimeConnectionLogic;

        #endregion

        #region Constructor

        public RealTimeConnectionController(IHttpContextAccessor accessor)
        {
            _realTimeConnectionLogic = new RealTimeConnectionLogic(accessor.HttpContext.User);
        }

        #endregion

        #region RealTimeConnection Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_realTimeConnectionLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_realTimeConnectionLogic.GetById(id));
        }

        #endregion

        #region RealTimeConnection Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RealTimeConnectionFindModel parameter)
        {
            return Result(_realTimeConnectionLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RealTimeConnectionFindModel parameter)
        {
            return Result(_realTimeConnectionLogic.FindAll(parameter));
        }

        #endregion

        #region RealTimeConnection Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RealTimeConnectionCreateModel entity)
        {
            return Result(_realTimeConnectionLogic.Add(entity));
        }

        #endregion

        #region RealTimeConnection Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RealTimeConnectionUpdateModel entity)
        {
            return Result(_realTimeConnectionLogic.Update(entity));
        }

        #endregion

        #region RealTimeConnection Remove



        #endregion
    }
}
