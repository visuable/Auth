using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Auth
{
    public class JWTAuthFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"];
            var typeAuth = token.ToArray()[0];
            if(typeAuth != "Bearer" || token == string.Empty)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            else
            {
                // Обратно восстанавливаем токен
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = Settings.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = Settings.GetSymmetricKey(),
                    ValidateIssuerSigningKey = true
                }, out var validatedToken);
                return;
            }
        }
    }
}
