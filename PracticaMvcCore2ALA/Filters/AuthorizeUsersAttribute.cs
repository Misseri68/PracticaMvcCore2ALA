﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticaMvcCore2ALA.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if(!user.Identity.IsAuthenticated)
            {
                context.Result = this.GetRoute("Managed", "Login");
            }
        }

        private RedirectToRouteResult GetRoute(string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(
                new { controller = controller, action = action }
            );

            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }
    }
}
