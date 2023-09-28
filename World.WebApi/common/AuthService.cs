using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace World.WebApi.common
{
    public static class AuthService
    {
        public static TokenValidationParameters GetTokenValidator(string issuer, string audience, string key)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ClockSkew = TimeSpan.Zero
            };
        }
        public static Claim? Authenticate(TokenValidationParameters tokenValidationParameters, JwtSecurityTokenHandler tokenHandler, string token)
        {
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtToken = (JwtSecurityToken)securityToken;
            Claim? claim = jwtToken.Claims.
                Where(x => x.Type == AutherizationConstant.HTTP_ALLOW_METHOD_PROPERTY).
                FirstOrDefault();
            return claim;

        }

        public static bool Authorize(Claim? claim, string method)
        {
            bool isSuccess = false;
            if (claim != null)
            {
                ISet<string> allowedActions = new HashSet<string>(claim.Value.Split(','));
                switch (method.ToLower())
                {
                    case "get":
                        isSuccess = allowedActions.Contains(ACTION.READ);
                        break;
                    case "post":
                        isSuccess = allowedActions.Contains(ACTION.INSERT);
                        break;
                    case "put":
                        isSuccess = allowedActions.Contains(ACTION.UPDATE);
                        break;
                    case "delete":
                        isSuccess = allowedActions.Contains(ACTION.DELETE);
                        break;
                    default:
                        break;
                }
            }
            return isSuccess;
        }
    }
}
