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
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Repositoriy.Interface;

namespace VolunteerProject.Application.Services
{
    public class PostsService : IPostsService 
    {
        private readonly IPostRepositoriy _postRepositoriy;

        public PostsService(IPostRepositoriy postRepositotiy)
        {
            _postRepositoriy = postRepositotiy;
        }

        public async Task<Result<IEnumerable<PostResponce>>> GetAllPosts()
        {
            var allPosts = await _postRepositoriy.GetAllPosts();
            
            if(allPosts == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed get all data from data base");
            }

            var allPostsReturn = new List<PostResponce>();

            foreach(var post in allPosts)
            {
                var postReturn = post.ToPostResponse();

                allPostsReturn.Add(postReturn);
            }

            if(allPostsReturn == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed create list with all posts");
            }

            return new SuccessResult<IEnumerable<PostResponce>>(allPostsReturn);
        }

        public async Task<Result<PostResponce>> AddPost(AddPostRequest postData)
        {
            var post = postData.ToPostAddRequest();

            if(post == null)
            {
                return new NotFoundResult<PostResponce>(default!);
            }

            var postCreate = await _postRepositoriy.CreatePost(post);
            
            if(postCreate != null)
            {
                return new SuccessResult<PostResponce>(postCreate.ToPostResponse());
            }

            return new NotFoundResult<PostResponce>("Error add in Data Base post!");
        }

        public async Task<Result<IEnumerable<PostResponce>>> GetPostByTitle(string titlePost)
        {
            var postByTitle = await _postRepositoriy.GetPostsByTitle(titlePost);

            if (postByTitle == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed get data from data base");
            }

            var postsReturn = new List<PostResponce>();

            foreach (var post in postByTitle)
            {
                var postReturn = post.ToPostResponse();

                postsReturn.Add(postReturn);
            }

            if (postsReturn == null)
            {
                return new NotFoundResult<IEnumerable<PostResponce>>("Failed create list with posts");
            }

            return new SuccessResult<IEnumerable<PostResponce>>(postsReturn);
        }

        public async Task<Result<string>> DeletePost(Guid Id)
        {
            var resultDeltePost = await _postRepositoriy.DeletePost(Id);

            if(resultDeltePost == null)
            {
                return new NotFoundResult<string>("Failed delete post");
            }

            return new SuccessResult<string>(default!);
        }

        public async Task<Result<PostResponce>> ChangeOfDataPost(PutPostRequest changePost)
        {
            var post = changePost.ToPostPutRequest();

            var resultChangePost = await _postRepositoriy.ChangePost(post);

            if(resultChangePost == null)
            {
                return new NotFoundResult<PostResponce>("Faile change data in data base");
            }

            var responceChangePost = resultChangePost.ToPostResponse();

            if(responceChangePost == null)
            {
                return new NotFoundResult<PostResponce>("Failed convert in models");
            }

            return new SuccessResult<PostResponce>(responceChangePost);
        }
    }
}
