using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Web.Decryptor;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("TestEndPoint")]
        public IActionResult TestRequest()
        {
            return this.GetResponse(new SuccessResult<string>(default!));
        }
    }
}
