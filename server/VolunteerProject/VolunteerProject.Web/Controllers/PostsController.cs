using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts() 
        {
            var allPosts = await _postService.GetAllPosts();
            return this.GetResponse(allPosts);
        }

        [HttpGet("GetPostByTitle")]
        public async Task<IActionResult> GetPostByTitle(string titlePost)
        {
            var postByTitle = await _postService.GetPostByTitle(titlePost);
            return this.GetResponse(postByTitle);
        }

        [HttpGet("GetAllPostsByUserName")]
        public async Task<IActionResult> GetAllPostByUserName(string userName)
        {
            var postByUserName = await _postService.GetAllPostByUserName(userName);
            return this.GetResponse(postByUserName);
        }

        [HttpPost("AddPost")]
        public async Task<IActionResult> AddNewPost([FromBody] AddPostRequest newPost)
        {
            var postData = await _postService.AddPost(newPost);
            return this.GetResponse(postData);
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(Guid Id)
        {
            var deletePostResult = await _postService.DeletePost(Id);
            return this.GetResponse(deletePostResult);
        }

        [HttpPut("ChangeOfDataPost")]
        public async Task<IActionResult> ChageOfDataPost([FromBody] PutPostRequest changePost)
        {
            var resultChangePost = await _postService.ChangeOfDataPost(changePost);
            return this.GetResponse(resultChangePost);
        }
    }
}
