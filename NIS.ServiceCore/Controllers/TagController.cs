using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Tag;
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
    public class TagController : BaseController
    {
        #region Properties

        private readonly TagLogic _tagLogic;

        #endregion

        #region Constructor

        public TagController(IHttpContextAccessor accessor)
        {
            _tagLogic = new TagLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Tag Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_tagLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_tagLogic.GetById(id));
        }

        #endregion

        #region Tag Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]TagFindModel parameter)
        {
            return Result(_tagLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]TagFindModel parameter)
        {
            return Result(_tagLogic.FindAll(parameter));
        }

        #endregion

        #region Tag Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]TagCreateModel entity)
        {
            return Result(_tagLogic.Add(entity));
        }

        #endregion

        #region Tag Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]TagUpdateModel entity)
        {
            return Result(_tagLogic.Update(entity));
        }

        #endregion

        #region Tag Remove



        #endregion
    }
}
