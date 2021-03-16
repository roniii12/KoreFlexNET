using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex
{
    public class IPMiddleware
    {
        private RequestDelegate next;
        private IMemoryCache _cache;
        private UserLogic userLogic;
        public IPMiddleware(RequestDelegate nextDelegate, IMemoryCache cache,UserLogic userLogic)
        {
            next = nextDelegate;
            _cache = cache;
            this.userLogic = userLogic;
        }
        public async Task Invoke(HttpContext context)
        {
            List<string> ipList;
            string requestIp = context.Request.HttpContext.Connection.RemoteIpAddress.ToString().Trim();
            if (_cache.TryGetValue("IpList", out ipList)){
                if (ipList.Contains(requestIp))
                {
                    await next(context);
                    return;
                }
            }
            string acceptIp = userLogic.getIpOfUser(context.User.Identity.Name);
            if (acceptIp == null || acceptIp == requestIp)
                await next(context);
            else
                throw new ArgumentException();
        }
    }
}
