using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.SourceType;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SourceTypeController : BaseController
    {
        #region Properties

        private readonly SourceTypeLogic _sourceTypeLogic;

        #endregion

        #region Constructor

        public SourceTypeController(IHttpContextAccessor accessor)
        {
            _sourceTypeLogic = new SourceTypeLogic(accessor.HttpContext.User);
        }

        #endregion


        #region SourceType Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_sourceTypeLogic.GetAll(filter));
            _sourceTypeLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_sourceTypeLogic.GetById(id));
            _sourceTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region SourceType Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]SourceTypeFindModel parameter)
        {
            var result = Result(_sourceTypeLogic.Find(parameter));
            _sourceTypeLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]SourceTypeFindModel parameter)
        {
            var result = Result(_sourceTypeLogic.FindAll(parameter));
            _sourceTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region SourceType Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]SourceTypeCreateModel entity)
        {
            var result = Result(_sourceTypeLogic.Add(entity));
            _sourceTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region SourceType Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]SourceTypeUpdateModel entity)
        {
            var result = Result(_sourceTypeLogic.Update(entity));
            _sourceTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region SourceType Remove



        #endregion
    }
}
