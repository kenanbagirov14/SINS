using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace NIS.ServiceCore.Services
{

    /// <summary>
    /// Handles all exception in the project throwed to API.
    /// </summary>
    public class GeneralExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// Handles error asynchoronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new InternalServerErrorResult(context.Request);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Handles error.
        /// </summary>
        /// <param name="context"></param>
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerErrorResult(context.Request);
        }

        /// <summary>
        /// Defines when should handle. I have set it always true according to our scenario. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}

