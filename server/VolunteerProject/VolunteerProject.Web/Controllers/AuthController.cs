using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Web.Decryptor;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Loggin([FromBody] UserLogingRequest loggingRequest)
        {
            var token = await _authService.LoginUser(loggingRequest);
            return this.GetResponse(token);
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationRequest registrationRequest)
        {
            var registerResponce = await _authService.RegisterUser(registrationRequest);
            return this.GetResponse(registerResponce);
        }

        [HttpPost("GetNewToken")]
        public async Task<IActionResult> GetNewAccessToken([FromBody] TokenRequest tokenRequest)
        {
            var newToken = await _authService.GetNewAccessToken(tokenRequest);
            return this.GetResponse(newToken);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(string accessToken)
        {
            var result = await _authService.Logout(accessToken);
            return this.GetResponse(result);
        }
        
    }
}
