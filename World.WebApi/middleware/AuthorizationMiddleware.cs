using System.Security.Claims;
using World.WebApi.common;

namespace World.WebApi.middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizationMiddleware> _logger;

        public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                Claim? claim = (Claim?)httpContext.Items[AutherizationConstant.HTTP_ALLOW_METHOD_PROPERTY];
                if (claim != null)
                {
                    _logger.LogInformation("Autherization claim Details @{claimValue}", claim.Value);
                    bool isAuthorized = AuthService.Authorize(claim, httpContext.Request.Method);
                    if (!isAuthorized)
                    {
                        throw new UnauthorizedAccessException("Method Not Allowed");
                    }
                }
                await _next(httpContext);
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException(e.Message);
            }
        }
    }
}
