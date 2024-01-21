using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Area;
using NIS.BLCore.Models.Core;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class AreaController : BaseController
    {
        #region Properties

        private readonly AreaLogic _areaLogic;

        #endregion

        #region Constructor

        public AreaController(IHttpContextAccessor accessor, ILogger<AreaController> logger)
        {
            logger.LogInformation("at logu");
            _areaLogic = new AreaLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Area Get
        [HttpGet("GetAll")]
        [ResponseType(typeof(IEnumerable<AreaViewModel>))]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_areaLogic.GetAll(filter));
            _areaLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_areaLogic.GetById(id));
            _areaLogic.Dispose();
            return result;
        }

        #endregion

        #region Area Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]AreaFindModel parameter)
        {
            var result = Result(_areaLogic.Find(parameter));
            _areaLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]AreaFindModel parameter)
        {
            var result = Result(_areaLogic.FindAll(parameter));
            _areaLogic.Dispose();
            return result;
        }

        #endregion

        #region Area Add
         
        [HttpPost("Add")]
        public IActionResult Add(AreaCreateModel entity)
        {
            var result = Result(_areaLogic.Add(entity));
            _areaLogic.Dispose();
            return result;
        }
         
        [HttpPost("AddRange")]
       public IActionResult AddRange([FromBody]List<AreaCreateModel> entities)
        {
            var result = Result(_areaLogic.AddRange(entities));
            _areaLogic.Dispose();
            return result;
        }

        #endregion

        #region Area Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]AreaUpdateModel entity)
        {
            var result = Result(_areaLogic.Update(entity));
            _areaLogic.Dispose();
            return result;
        }

        #endregion

        #region Area Remove



        #endregion
    }
}
