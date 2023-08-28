using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Web.Decryptor;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUserByUserName")]
        public async Task<IActionResult> GetAllUserByUserName(string userName) 
        {
            var userNameList = await _userService.GetUserNameByTitle(userName);
            return this.GetResponse(userNameList);
        }
    }
}
