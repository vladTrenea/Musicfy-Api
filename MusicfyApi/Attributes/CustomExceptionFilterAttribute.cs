using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Musicfy.Bll.Models;
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
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.Unauthorized, new ErrorModel(exception.Message));

                return;
            }

            if (exception is NotFoundException)
            {
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.NotFound, new ErrorModel(exception.Message));

                return;
            }

            if (exception is ValidationException)
            {
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, new ErrorModel(exception.Message));

                return;
            }

            if (exception is ConflictException)
            {
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.Conflict, new ErrorModel(exception.Message));

                return;
            }

            actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorModel(Messages.InternalServerError));
            Logger.Error(exception, Messages.InternalServerError);
        }
    }
}