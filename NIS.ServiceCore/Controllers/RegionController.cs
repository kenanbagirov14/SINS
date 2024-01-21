using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Region;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RegionController : BaseController
    {
        #region Properties

        private readonly RegionLogic _regionLogic;

        #endregion

        #region Constructor

        public RegionController(IHttpContextAccessor accessor)
        {
            _regionLogic = new RegionLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Region Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_regionLogic.GetAll(filter));
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            return Result(_regionLogic.GetById(id));
        }

        #endregion

        #region Region Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RegionFindModel parameter)
        {
            return Result(_regionLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RegionFindModel parameter)
        {
            return Result(_regionLogic.FindAll(parameter));
        }

        #endregion

        #region Region Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RegionCreateModel parameter)
        {
            return Result(_regionLogic.Add(parameter));
        }

        #endregion

        #region Region Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RegionUpdateModel parameter)
        {
            return Result(_regionLogic.Update(parameter));
        }

        #endregion

        #region Region Remove



        #endregion
    }
}
