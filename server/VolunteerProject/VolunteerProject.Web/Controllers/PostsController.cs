using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerProject.Application.DTOs.PostDTOs;
using VolunteerProject.Application.DTOs.PostDTOs.Request;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Web.Decryptor;

namespace VolunteerProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postService;

        public PostsController(IPostsService postsService)
        {
            _postService = postsService;
        }
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddNewPost([FromBody] AddPostRequest newPost)
        {
            var postData = await _postService.AddPost(newPost);
            return this.GetResponse(postData);
        }
    }
}
