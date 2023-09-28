using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using World.WebApi.common;

namespace World.WebApi.middleware
{
    public class AuthenticationMiddleware
    {
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public AuthenticationMiddleware(RequestDelegate next,
                                        [NotNull] IConfiguration _configuration,
                                        ILogger<AuthenticationMiddleware> logger
            )
        {
#pragma warning disable CS8604 // Possible null reference argument.
            tokenValidationParameters = AuthService.GetTokenValidator(issuer: _configuration["JWTValidator:Issuer"],
                                                                     _configuration["JWTValidator:Audience"],
                                                                     _configuration["JWTValidator:SigningKey"]);
#pragma warning restore CS8604 // Possible null reference argument.
            tokenHandler = new JwtSecurityTokenHandler();
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("Starting request Method :- {@RequestMethod} RequestBody:- {@RequestBody} dateTime:- {@DateTimeUTC}",
                 httpContext.Request.Method,
                 httpContext.Request.Body,
                  DateTime.UtcNow
                  );
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string headerToken = httpContext.Request.Headers["Authorization"];
                if (headerToken != null)
                {
                    string? token = headerToken?.Split(' ').LastOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                    Claim? claim = AuthService.Authenticate(tokenValidationParameters, tokenHandler, token);
#pragma warning restore CS8604 // Possible null reference argument.
                    httpContext.Items[AutherizationConstant.HTTP_ALLOW_METHOD_PROPERTY] = claim;
                }
                await _next(httpContext);

                _logger.LogInformation("Completed request Method :- {@RequestMethod} RequestBody:- {@RequestBody} dateTime:- {@DateTimeUTC}",
                 httpContext.Request.Method,
                 httpContext.Request.Body,
                  DateTime.UtcNow
                  );
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException(e.Message);
            }
        }
    }
}
