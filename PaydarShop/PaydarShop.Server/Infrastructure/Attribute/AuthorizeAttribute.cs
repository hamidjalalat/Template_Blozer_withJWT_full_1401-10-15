using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaydarShop.Server.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute :System.Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {

        }
        public AuthorizeAttribute(string role )
        {
            Role = role;
        }
        public string Role { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var rol = Role;
            Models.User user = context.HttpContext.Items["User"] as Models.User;
            
            if (user==null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.
                    JsonResult(new { message = "unauthorized" })
                {
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized
                };
            }
           
        }
    }
}
