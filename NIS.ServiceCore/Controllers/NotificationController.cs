using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.Notification;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : BaseController
    {
        #region Properties

        private readonly NotificationLogic _NotificationLogic;

        #endregion

        #region Constructor

        public NotificationController(IHttpContextAccessor accessor)
        {
            _NotificationLogic = new NotificationLogic(accessor.HttpContext.User);
        }

        #endregion


        #region Notification Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_NotificationLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_NotificationLogic.GetById(id));
        }

        #endregion

        #region Notification Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]NotificationFindModel parameter)
        {
            return Result(_NotificationLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]NotificationFindModel parameter)
        {
            return Result(_NotificationLogic.FindAll(parameter));
        }

        #endregion

        #region Notification Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]NotificationCreateModel parameter)
        {
            return Result(_NotificationLogic.Add(parameter));
        }

        #endregion

        #region Notification Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]NotificationUpdateModel parameter)
        {
            return Result(_NotificationLogic.Update(parameter));
        }

        #endregion

        #region Notification Remove



        #endregion
    }
}
