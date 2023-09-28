using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using World.WebApi.common;

namespace World.WebApi.filters
{
    public class AuthorizationTokenFilter : IAuthorizationFilter
    {
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly IConfiguration? _configuration;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public AuthorizationTokenFilter(IConfiguration? configuration)
        {
            _configuration = configuration;
#pragma warning disable CS8604 // Possible null reference argument.
            tokenValidationParameters = AuthService.GetTokenValidator(_configuration["JWTValidator:Issuer"],
                                                                     _configuration["JWTValidator:Audience"],
                                                                     _configuration["JWTValidator:SigningKey"]);
#pragma warning restore CS8604 // Possible null reference argument.
            tokenHandler = new JwtSecurityTokenHandler();
        }
        #region private method
        #endregion
        #region Public Method
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                KeyValuePair<string, StringValues>? keyValuePair = context.
                    HttpContext.
                    Request.
                    Headers.
                    SingleOrDefault(x => x.Key == "UserName");

                string? headerToken = context.HttpContext.Request.Headers["Authorization"];
                if (headerToken != null)
                {
                    string? token = headerToken.Split(' ').LastOrDefault();
                    // tokenValidationParameters = GetTokenValidator();
                    var tokenHandler = new JwtSecurityTokenHandler();
#pragma warning disable CS8604 // Possible null reference argument.
                    Claim? claim = AuthService.Authenticate(tokenValidationParameters, tokenHandler, token);
#pragma warning restore CS8604 // Possible null reference argument.
                    context.HttpContext.Items[AutherizationConstant.HTTP_ALLOW_METHOD_PROPERTY] = claim;
                }
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(MessageConstants.InvalidAccessToken, ex);
            }

        }
        #endregion
    }
}
