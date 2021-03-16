using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex.Filters
{
    public class AuthorizeIp: AllowAnonymousAttribute, IAuthorizationFilter
    {
        private IMemoryCache _cache;
        private UserLogic userLogic;
        public AuthorizeIp(IMemoryCache cache, UserLogic userLogic)
        {
            _cache = cache;
            this.userLogic = userLogic;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var temp = context.ActionDescriptor.EndpointMetadata;
            foreach (var typeAction in temp)
            {
                if (typeAction.GetType().Equals(typeof(AllowAnonymousAttribute)))
                    return;
            }
            try
            {
                List<string> ipList;
                string requestIp = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString().Trim();
                if (_cache.TryGetValue("IpList", out ipList))
                {
                    if (ipList.Contains(requestIp))
                    {
                        return;
                    }
                }
                string acceptIp = userLogic.getIpOfUser(context.HttpContext.User.Identity.Name);
                if (acceptIp == requestIp)
                {
                    if (ipList == null) ipList = new List<string>() { requestIp };
                    else ipList.Add(requestIp);
                    _cache.Set<List<string>>("IpList", ipList, new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30),
                        SlidingExpiration = TimeSpan.FromHours(6),
                    });
                    return;
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch (Exception e)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    errorMsg = e.Message
                });
            }
        }
    }
}
