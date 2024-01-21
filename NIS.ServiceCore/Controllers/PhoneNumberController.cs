using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Core;
using NIS.BLCore.Models.PhoneNumber;

using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PhoneNumberController : BaseController
    {
        #region Properties

        private readonly PhoneNumberLogic _phoneNumberLogic;

        #endregion

        #region Constructor

        public PhoneNumberController(IHttpContextAccessor accessor)
        {
            _phoneNumberLogic = new PhoneNumberLogic(accessor.HttpContext.User);
        }

        #endregion

        #region PhoneNumber Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_phoneNumberLogic.GetAll(filter));
            _phoneNumberLogic.Dispose();
            return  result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_phoneNumberLogic.GetById(id));
            _phoneNumberLogic.Dispose();
            return result;
        }

        #endregion

        #region PhoneNumber Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]PhoneNumberFindModel parameter)
        {
            var result = Result(_phoneNumberLogic.Find(parameter));
            _phoneNumberLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]PhoneNumberFindModel parameter)
        {
            var result = Result(_phoneNumberLogic.FindAll(parameter));
            _phoneNumberLogic.Dispose();
            return result;
        }

        #endregion

        #region PhoneNumber Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]PhoneNumberCreateModel entity)
        {
            var result = Result(_phoneNumberLogic.Add(entity));
            _phoneNumberLogic.Dispose();
            return result;
        }

        #endregion

        #region PhoneNumber Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]PhoneNumberUpdateModel entity)
        {
            var result = Result(_phoneNumberLogic.Update(entity));
            _phoneNumberLogic.Dispose();
            return result;
        }

        #endregion

        #region PhoneNumber Remove

        [HttpPost("Remove")]
        public IActionResult Remove([FromBody]PhoneNumberDeleteModel entity)
        {
            var result = Result(_phoneNumberLogic.Remove(entity));
            _phoneNumberLogic.Dispose();
            return result;
        }

        #endregion
    }
}
