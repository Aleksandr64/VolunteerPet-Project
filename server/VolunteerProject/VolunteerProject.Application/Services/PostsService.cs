using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.PostDTOs;
using VolunteerProject.Application.DTOs.PostDTOs.Request;
using VolunteerProject.Application.DTOs.PostDTOs.Responce;
using VolunteerProject.Application.Mappers;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.Models;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Repositoriy.Interface;

namespace VolunteerProject.Application.Services
{
    public class PostsService : IPostsService 
    {
        private readonly IPostRepositoriy _postRepositoriy;
        private readonly UserManager<User> _userManager;

        public PostsService(
            IPostRepositoriy postRepositotiy, 
            UserManager<User> userManager
            )
        {
            _postRepositoriy = postRepositotiy;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<PostResponce>>> GetAllPosts()
        {
            var allPosts = await _postRepositoriy.GetAllPosts();
            
            if(allPosts == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed get all data from data base");
            }

            var allPostsReturn = allPosts.Select(x => x.ToPostResponse());

            return new SuccessResult<IEnumerable<PostResponce>>(allPostsReturn);
        }

        public async Task<Result<PostResponce>> AddPost(AddPostRequest postData)
        {
            var user = await _userManager.FindByNameAsync(postData.UserName);

            if(user == null)
            {
                return new NotFoundResult<PostResponce>("Failed, User with this user name doesn't exist");
            }

            var post = postData.ToPostAddRequest(user);

            var postCreate = await _postRepositoriy.CreatePost(post);

            return new SuccessResult<PostResponce>(postCreate.ToPostResponse());
        }

        public async Task<Result<IEnumerable<PostResponce>>> GetPostByTitle(string titlePost)
        {
            var postByTitle = await _postRepositoriy.GetPostsByTitle(titlePost);

            if (postByTitle == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed get data from data base");
            }

            var postsReturn = postByTitle.Select(x => x.ToPostResponse());

            return new SuccessResult<IEnumerable<PostResponce>>(postsReturn);
        }

        public async Task<Result<PostResponce>> DeletePost(Guid Id)
        {
            await _postRepositoriy.DeletePost(Id);
            return new SuccessResult<PostResponce>(default!);
        }

        public async Task<Result<PostResponce>> ChangeOfDataPost(PutPostRequest changePost)
        {
            var user = await _userManager.FindByNameAsync(changePost.UserName);

            if (user == null)
            {
                return new NotFoundResult<PostResponce>("Failed, User with this user name doesn't exist");
            }

            var post = changePost.ToPostPutRequest(user);

            var resultChangePost = await _postRepositoriy.ChangePost(post);

            if(resultChangePost == null)
            {
                return new NotFoundResult<PostResponce>("Faile change data in data base");
            }

            var responceChangePost = resultChangePost.ToPostResponse();

            return new SuccessResult<PostResponce>(responceChangePost);
        }

        public async Task<Result<IEnumerable<PostResponce>>> GetAllPostByUserName(string userName)
        {
            var user = await _postRepositoriy.GetAllPostsByUserName(userName);

            if(user == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("User with this nickname was not found.");
            }
            
            var postCollectionOfUserName = user.Posts.ToList();

            if(!postCollectionOfUserName.Any())
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("This user don't have Posts");
            }

            var postByUserName = postCollectionOfUserName.Select(x => x.ToPostResponse());

            return new SuccessResult<IEnumerable<PostResponce>>(postByUserName);
        }
    }
}
