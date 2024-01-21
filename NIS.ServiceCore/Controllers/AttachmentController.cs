using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore.Logics;
using NIS.BLCore.Models.Attachment;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NIS.ServiceCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentController : BaseController
    {
        #region Properties

        private readonly AttachmentLogic _attachmentLogic;

        #endregion

        #region Constructor

        public AttachmentController(IHostingEnvironment env,IHttpContextAccessor accessor)
        {
            _attachmentLogic = new AttachmentLogic( env, accessor.HttpContext.User);
        }

        #endregion


        #region Attachment Add

        [HttpPost("Add")]
        public IActionResult Add([FromBody]AttachmentCreateModel entity)
        {
            var result = Result(_attachmentLogic.Add(entity));
            _attachmentLogic.Dispose();
            return result;
        }

        #endregion

    }
}
