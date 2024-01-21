using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.UserSettings;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserSettingsController : BaseController
    {
        #region Properties

        private readonly UserSettingsLogic _userSettingsLogic;

        #endregion

        #region Constructor

        public UserSettingsController(IHttpContextAccessor accessor)
        {
            _userSettingsLogic = new UserSettingsLogic(accessor.HttpContext.User);
        }

        #endregion

        #region UserSettings Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_userSettingsLogic.GetAll(filter));
            _userSettingsLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_userSettingsLogic.GetById(id));
            _userSettingsLogic.Dispose();
            return result;
        }

        #endregion

        #region UserSettings Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]UserSettingsFindModel parameter)
        {
            var result = Result(_userSettingsLogic.Find(parameter));
            _userSettingsLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]UserSettingsFindModel parameter)
        {
            var result = Result(_userSettingsLogic.FindAll(parameter));
            _userSettingsLogic.Dispose();
            return result;
        }

        #endregion

        #region UserSettings Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]UserSettingsCreateModel entity)
        {
            var result = Result(_userSettingsLogic.Add(entity));
            _userSettingsLogic.Dispose();
            return result;
        }

        #endregion

        #region UserSettings Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]UserSettingsUpdateModel entity)
        {
            var result = Result(_userSettingsLogic.Update(entity));
            _userSettingsLogic.Dispose();
            return result;
        }

        #endregion

        #region UserSettings Remove


        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]UserSettingsDeleteModel entity)
        {
            var result = Result(_userSettingsLogic.Remove(entity));
            _userSettingsLogic.Dispose();
            return result;
        }

        #endregion
    }
}
