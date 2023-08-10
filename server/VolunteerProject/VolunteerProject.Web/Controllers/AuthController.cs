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

        [HttpPost("Loggin")]
        public async Task<IActionResult> Loggin([FromBody] UserLogingRequest loggingRequest)
        {
            return Ok();
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationRequest registrationRequest)
        {
            var registerResponce = await _authService.RegisterUser(registrationRequest);
            return this.GetResponse(registerResponce);
        }
    }
}
