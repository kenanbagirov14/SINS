using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Rating;
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
    public class RatingController : BaseController
    {
        #region Properties

        private readonly RatingLogic _ratingLogic;

        #endregion

        #region Constructor

        public RatingController(IHttpContextAccessor accessor)
        {
            _ratingLogic = new RatingLogic(accessor.HttpContext.User);
        }

        #endregion

        #region Rating Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            return Result(_ratingLogic.GetAll(filter));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Result(_ratingLogic.GetById(id));
        }

        #endregion

        #region Rating Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]RatingFindModel parameter)
        {
            return Result(_ratingLogic.Find(parameter));
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]RatingFindModel parameter)
        {
            return Result(_ratingLogic.FindAll(parameter));
        }

        #endregion

        #region Rating Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]RatingCreateModel entity)
        {
            return Result(_ratingLogic.Add(entity));
        }

        #endregion

        #region Rating Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]RatingUpdateModel entity)
        {
            return Result(_ratingLogic.Update(entity));
        }

        #endregion

        #region Rating Remove



        #endregion
    }
}
