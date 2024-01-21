using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;using Microsoft.AspNetCore.Http;
using NIS.BLCore; 

namespace NIS.ServiceCore.Helpers
{
    public class CreateResult<TResult>  where TResult : class
    {
        private readonly HttpStatusCode _statusCode;
        private readonly LogicResult<TResult> _result;
        private readonly ObjectResult _request;

        public  CreateResult(LogicResult<TResult> result, ObjectResult request)
        {
            _result = result;
            _request = request;
            _statusCode = (result.ErrorList.Exists(error => result.ErrorList.Count > 0))
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.OK;
        }

        public ObjectResult CreateResponse()
        {
            _request.StatusCode = (int)_statusCode;
            return _request;
        }
        
    }
}