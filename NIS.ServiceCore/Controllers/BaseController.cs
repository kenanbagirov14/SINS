using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using log4net;
using Microsoft.AspNetCore.Mvc;
using NIS.BLCore;
using NIS.ServiceCore.Helpers;
using NIS.ServiceCore.Services;

namespace NIS.ServiceCore.Controllers
{

    public class BaseController : ControllerBase
    {
        internal readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetExceptionLogger));
        public ObjectResult Result<TResult>(LogicResult<TResult> result) where TResult : class
        {
            return new CreateResult<TResult>(result, new ObjectResult(result)).CreateResponse();
        }


    }
}
