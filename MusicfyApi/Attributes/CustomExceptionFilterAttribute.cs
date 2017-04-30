using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Musicfy.Infrastructure.Configs;
using Musicfy.Infrastructure.Exceptions;
using Musicfy.Infrastructure.Resources;
using Serilog;

namespace MusicfyApi.Attributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly string LogPath = AppDomain.CurrentDomain.BaseDirectory + Config.LogFilePath;

        private static readonly ILogger Logger = new LoggerConfiguration()
            .WriteTo.RollingFile(LogPath)
            .CreateLogger();

        /// <summary>
        /// Assign status code to exceptions thrown
        /// </summary>
        /// <param name="actionExecutedContext">Context</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var request = actionExecutedContext.Request;

            if (exception is UnauthorizedException)
            {
                actionExecutedContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, exception.Message);

                return;
            }

            if (exception is NotFoundException)
            {
                actionExecutedContext.Response = request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);

                return;
            }

            if (exception is ValidationException)
            {
                actionExecutedContext.Response = request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);

                return;
            }

            if (exception is ConflictException)
            {
                actionExecutedContext.Response = request.CreateErrorResponse(HttpStatusCode.Conflict, exception.Message);

                return;
            }

            actionExecutedContext.Response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, Messages.InternalServerError);
            Logger.Error(exception, Messages.InternalServerError);
        }
    }
}