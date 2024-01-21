using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.WorkFlow;
using NIS.BLCore.Models.Core;
using Microsoft.AspNetCore.Mvc;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class WorkFlowController : BaseController
    {
        #region Properties

        private readonly WorkFlowLogic _WorkFlowLogic;

        #endregion

        #region Constructor

        public WorkFlowController(IHttpContextAccessor accessor)
        {
            _WorkFlowLogic = new WorkFlowLogic(accessor.HttpContext.User);
        }

        #endregion

        #region WorkFlow Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_WorkFlowLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_WorkFlowLogic.GetById(id));
        }

        #endregion

        #region WorkFlow Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]WorkFlowFindModel parameter)
        {
            return Result(_WorkFlowLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]WorkFlowFindModel parameter)
        {
            return Result(_WorkFlowLogic.FindAll(parameter));
        }

        #endregion

        #region WorkFlow Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]WorkFlowCreateModel entity)
        {
            return Result(_WorkFlowLogic.Add(entity));
        }

        #endregion

        #region WorkFlow Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]WorkFlowUpdateModel entity)
        {
            return Result(_WorkFlowLogic.Update(entity));
        }

        #endregion

        #region WorkFlow Remove



        #endregion
    }
}
