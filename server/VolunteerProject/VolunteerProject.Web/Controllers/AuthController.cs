using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        

        [HttpPost("Loggin")]
        public async Task<IActionResult> Loggin([FromBody] UserLogingRequest loggingRequest)
        {
            return Ok();
        }


        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationRequest registrationRequest)
        {
            return Ok();
        }
    }
}
