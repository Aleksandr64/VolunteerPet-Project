using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Web.Decryptor;
using VolunteerProject.Web.CustomAttribute;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize("Admin")]
        [HttpGet("TestEndPoint")]
        public IActionResult TestRequest()
        {
            return this.GetResponse(new SuccessResult<string>(default!));
        }
    }
}
