using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NIS.BLCore.Hubs;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Comment;
using NIS.BLCore.Models.Core;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : BaseController
    {
        #region Properties

        private readonly CommentLogic _commentLogic;

        private readonly IHubContext<NisHub> _hubContext;
        #endregion

        #region Constructor

        public CommentController(IHttpContextAccessor accessor, IHubContext<NisHub> hubContext)
        {
            _hubContext = hubContext;
            _commentLogic = new CommentLogic(accessor.HttpContext.User,hubContext);
        }

        #endregion

        #region Comment Get

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromUri]Filter filter)
        {
            var result = Result(_commentLogic.GetAll(filter));
            _commentLogic.Dispose();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = Result(_commentLogic.GetById(id));
            _commentLogic.Dispose();
            return result;
        }

        #endregion

        #region Comment Find

        [HttpPost("Find")]
        public IActionResult Find([FromBody]CommentFindModel parameter)
        {
            var result = Result(_commentLogic.Find(parameter));
            _commentLogic.Dispose();
            return result;
        }

        [HttpPost("FindAll")]
        public IActionResult FindAll([FromBody]CommentFindModel parameter)
        {
            var result = Result(_commentLogic.FindAll(parameter));
            _commentLogic.Dispose();
            return result;
        }

        #endregion

        #region Comment Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]CommentCreateModel entity)
        {
            var result = Result(_commentLogic.Add(entity));
            _commentLogic.Dispose();
            return result;
        }

        #endregion

        #region Comment Update

        [HttpPost("Update")]
        public IActionResult Update([FromBody]CommentUpdateModel entity)
        {
            var result = Result(_commentLogic.Update(entity));
            _commentLogic.Dispose();
            return result;
        }

        #endregion

        #region Comment Remove



        #endregion
    }
}
