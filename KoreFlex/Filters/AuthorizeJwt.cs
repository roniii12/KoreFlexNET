using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreFlex.Filters
{
    public class AuthorizeJwt : AllowAnonymousAttribute, IAuthorizationFilter
    {
        private readonly JwtKeys jwtKeys;

        public AuthorizeJwt(IOptions<JwtKeys> options)
        {
            jwtKeys = options.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var temp = context.ActionDescriptor.EndpointMetadata;
            foreach (var typeAction in temp)
            {
                if(typeAction.GetType().Equals(typeof(AllowAnonymousAttribute)))
                    return;
            }
            try
            {
                string tokenHead = context.HttpContext.Request.Headers["Authorization"];
                tokenHead = tokenHead.Replace("Bearer ", "");
                //string token = tokenHead.ToString();
                var tokenHandler = new JwtSecurityTokenHandler();
                var validParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(jwtKeys.JwtSecret)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken securityToken; // = tokenHandler.ReadToken(tokenHead) as JwtSecurityToken;
                var valid = tokenHandler.ValidateToken(tokenHead, validParams, out securityToken);
                var securityTokenJwt = securityToken as JwtSecurityToken;
                var stringClaimValue = securityTokenJwt.Claims.First(claim => claim.Type == "unique_name").Value;
                var str = stringClaimValue;
            }
            catch(Exception e)
            {
                //context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new UnauthorizedObjectResult(new
                {
                    success = false
                });//(StatusCodes.Status401Unauthorized);
            }
        }
    }
    public class JwtKeys
    {
        public string JwtSecret { get; set; }
    }
}
