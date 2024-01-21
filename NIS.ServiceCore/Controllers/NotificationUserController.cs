using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.NotificationUser;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationUserController : BaseController
    {
        #region Properties

        private readonly NotificationUserLogic _NotificationUserLogic;

        #endregion

        #region Constructor

        public NotificationUserController(IHttpContextAccessor accessor)
        {
            _NotificationUserLogic = new NotificationUserLogic(accessor.HttpContext.User);
        }

        #endregion


        #region NotificationUser Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_NotificationUserLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_NotificationUserLogic.GetById(id));
        }

        #endregion

        #region NotificationUser Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]NotificationUserFindModel parameter)
        {
            return Result(_NotificationUserLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]NotificationUserFindModel parameter)
        {
            return Result(_NotificationUserLogic.FindAll(parameter));
        }

        #endregion

        #region NotificationUser Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]NotificationUserCreateModel parameter)
        {
            return Result(_NotificationUserLogic.Add(parameter));
        }

        #endregion

        #region NotificationUser Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]NotificationUserUpdateModel parameter)
        {
            return Result(_NotificationUserLogic.Update(parameter));
        }

        [HttpPost("UpdateRange")]
        public IActionResult UpdateRange([FromBody]NotificationUserUpdateModel parameter)
        {
            return Result(_NotificationUserLogic.UpdateRange(parameter));
        }

        #endregion

        #region NotificationUser Remove

        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]NotificationUserDeleteModel parameter)
        {
            return Result(_NotificationUserLogic.Remove(parameter));
        }

        #endregion
    }
}
