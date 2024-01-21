using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.InjuryType;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class InjuryTypeController : BaseController
    {
        #region Properties

        private readonly InjuryTypeLogic _injuryTypeLogic;

        #endregion

        #region Constructor

        public InjuryTypeController(IHttpContextAccessor accessor)
        {
            _injuryTypeLogic = new InjuryTypeLogic(accessor.HttpContext.User);
        }

        #endregion

        #region InjuryType Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_injuryTypeLogic.GetAll(filter));
            _injuryTypeLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_injuryTypeLogic.GetById(id));
            _injuryTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region InjuryType Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]InjuryTypeFindModel parameter)
        {
            var result = Result(_injuryTypeLogic.Find(parameter));
            _injuryTypeLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]InjuryTypeFindModel parameter)
        {
            var result = Result(_injuryTypeLogic.FindAll(parameter));
            _injuryTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region InjuryType Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]InjuryTypeCreateModel entity)
        {
            var result = Result(_injuryTypeLogic.Add(entity));
            _injuryTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region InjuryType Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]InjuryTypeUpdateModel entity)
        {
            var result = Result(_injuryTypeLogic.Update(entity));
            _injuryTypeLogic.Dispose();
            return result;
        }

        #endregion

        #region InjuryType Remove



        #endregion
    }
}
