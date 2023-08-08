using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("TestEndPoint")]
        public IActionResult TestRequest()
        {
            return Ok();
        }
    }
}
