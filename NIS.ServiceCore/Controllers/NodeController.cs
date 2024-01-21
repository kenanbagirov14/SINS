using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Node;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NodeController : BaseController
    {
        #region Properties

        private readonly NodeLogic _nodeLogic;

        #endregion

        #region Constructor

        public NodeController(IHttpContextAccessor accessor)
        {
            _nodeLogic = new NodeLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Node Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_nodeLogic.GetAll(filter));
            _nodeLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_nodeLogic.GetById(id));
            _nodeLogic.Dispose();
            return result;
        }

        #endregion

        #region Node Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]NodeFindModel parameter)
        {
            var result = Result(_nodeLogic.Find(parameter));
            _nodeLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]NodeFindModel parameter)
        {
            var result = Result(_nodeLogic.FindAll(parameter));
            _nodeLogic.Dispose();
            return result;
        }

        #endregion

        #region Node Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]NodeCreateModel entity)
        {
            var result = Result(_nodeLogic.Add(entity));
            _nodeLogic.Dispose();
            return result;
        }

        #endregion

        #region Node Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]NodeUpdateModel entity)
        {
            var result = Result(_nodeLogic.Update(entity));
            _nodeLogic.Dispose();
            return result;
        }

        #endregion

        #region Node Remove



        #endregion
    }
}
