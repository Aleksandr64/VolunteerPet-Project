using VolunteerProject.Application.Services.Interface;

namespace VolunteerProject.Web.CustomMiddlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userRoles = authService.ValidateToken(accessToken);

            if(userRoles != default)
            {
                context.Items["Roles"] = userRoles;
            }

            await _next(context);
        }
    }
}
