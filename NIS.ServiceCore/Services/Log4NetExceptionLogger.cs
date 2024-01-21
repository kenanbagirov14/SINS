using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace NIS.ServiceCore.Services
{
  /// <summary>
  /// Logs project exception wit log4net.
  /// </summary>
  public class Log4NetExceptionLogger : ExceptionLogger
  {
    private readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetExceptionLogger));

    /// <summary>
    /// Logs error asynchronously.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
    {
      var content = await context.Request.Content.ReadAsStringAsync();
      var ex = context.Exception;
      while (context.Exception.InnerException != null)
         ex = ex?.InnerException;
      _logger.Fatal(
          $"{Environment.NewLine}" +
          "Unhandled Exception! Exception: " +  //TODO: Exception must be illustrative!!
          $"{Environment.NewLine}" +
          $"{ex?.Message}," +
          $"{Environment.NewLine}" +
          $"Method: {context.Request.Method}" +
          $"{Environment.NewLine}" +
          $"Content: {content}" +
          $"{Environment.NewLine}" +
          $"Uri: {context.Request.RequestUri}" +
          $"{Environment.NewLine}" +
          $"Headers: {context.Request.Headers}" +
          $"{Environment.NewLine}" +
          $"{Environment.NewLine}" +
          $"Properties: {context.Request.Properties}" +
          $"{Environment.NewLine}" +
          $"Properties: {context.Request.Content}"
      );
    }

    /// <summary>
    /// Logs error.
    /// </summary>
    /// <param name="context"></param>
    public override void Log(ExceptionLoggerContext context)
    {
      var content = context.Request.Content.ReadAsStringAsync();
      var ex = context.Exception;
      while (context.Exception.InnerException != null)
        ex = ex?.InnerException;
      _logger.Fatal(
          $"{Environment.NewLine}," +
          "Unhandled Exception! Exception: " +
        $"{Environment.NewLine}" +
          $"{ex?.Message}," +
          $"{Environment.NewLine}" +
          $"Method: {context.Request.Method}" +
          $"{Environment.NewLine}" +
          $"Content: {content}" +
          $"{Environment.NewLine}" +
          $"Uri: {context.Request.RequestUri}" +
          $"{Environment.NewLine}" +
          $"Headers: {context.Request.Headers}" +
          $"{Environment.NewLine}" +
          $"Properties: {context.Request.Properties}"
      );
    }

  }
}

