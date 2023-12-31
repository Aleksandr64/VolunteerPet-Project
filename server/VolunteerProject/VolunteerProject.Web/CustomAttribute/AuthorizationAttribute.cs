﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Web.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserRolesEnum[] _roles;
        public AuthorizationAttribute()
        {

        }
        public AuthorizationAttribute(params UserRolesEnum[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return; 
            }

            var userRole = context.HttpContext.Items["Roles"];

            if(_roles != null && userRole != null)
            {   
                bool checkUserRole = _roles.Any(x => Enum.GetName(typeof(UserRolesEnum), x) == (string)userRole);
                if (!checkUserRole)
                {
                    context.Result = new JsonResult(new { message = "This user don't have access" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }

            if(userRole == null)
            {
                context.Result = new JsonResult(new { message = "Expired time JWT Token the end" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
