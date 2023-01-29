using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PaydarShop.Server.Infrastructure;
using PaydarShop.Server.Infrastructure.applicationsettings;
using PaydarShop.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaydarShop.Server.Middleware
{
    public class JwtMiddleware
    {
    

        public JwtMiddleware(RequestDelegate next, IOptions<Main> options)
        {
            Next = next;
            Options = options.Value;
        }

        protected  RequestDelegate Next;
        protected  Main  Options;

        public async Task Invoke(HttpContext context, IUserService userService)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrWhiteSpace(token)==false)
            {
                JwtUtility.attachUserToContextByToken(context,userService,token, Options.SecretKey);
            }

            await Next(context);
        }

    
    }
}
