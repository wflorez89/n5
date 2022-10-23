using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WilmerFlorez.Common.Responses;
using WilmerFlorez.Common.Audit;
using WilmerFlorez.Common.Exceptions;

namespace WilmerFlorez.Utilities.Implementation.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            try
            {
                _logger.LogInformation(new AuditInfo { Context = httpContext }.ToString());
                await _next(httpContext);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.NotModified);
            }
            catch (DataException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.Conflict);
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.InternalServerError);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            ErrorResponse response = new ErrorResponse()
            {
                Status = statusCode,
                Message = exception.Message
            };
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
        }
    }
}
