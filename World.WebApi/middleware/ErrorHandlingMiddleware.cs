using Newtonsoft.Json;
using System.Net;
using World.WebApi.common;

namespace World.WebApi.middleware
{
    public class ErrorHandlingMiddleware
    {
        #region Private Variables
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next">It will get populated by framework</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // call next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle exception and provide appropriate response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            switch (exception.Message)
            {
                case MessageConstants.UnauthorizedAccess:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = MessageConstants.UnauthorizedAccessException });
                    _logger.LogError("RolePermissionFilter OnActionExecuting @{ExceptionMessage}",
                        exception.InnerException == null ? exception.Message : exception.InnerException.Message);
                    break;
                case MessageConstants.InvalidAccessToken:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = MessageConstants.InvalidAccessTokenException });
                    _logger.LogError("AuthorizationTokenFilter OnAuthorization",
                        exception.InnerException == null ? exception.Message : exception.InnerException.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
        #endregion
    }
}
