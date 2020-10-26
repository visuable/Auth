using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth
{
    public class JWTAuthFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var values);
            string token = "";
            // Временно.
            var vals = values.ToString().Split(' ');
            if (vals[0] == JwtBearerDefaults.AuthenticationScheme)
            {
                token = vals[1];
            }
            if (token == string.Empty)
            {
                context.Result = new UnauthorizedResult();

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
