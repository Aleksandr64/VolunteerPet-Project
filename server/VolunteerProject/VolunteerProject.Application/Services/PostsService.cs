using System;
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

        public async Task<Result<GetPostResponce>> AddPost(AddPostRequest postData)
        {
            var post = postData.ToPostRequest();

            if(post == null)
            {
                return new NotFoundResult<GetPostResponce>(default!);
            }

            var postCreate = _postRepositoriy.CreatePost(post);
            
            if(postCreate != null)
            {
                return new SuccessResult<GetPostResponce>(postCreate.ToPostResponse());
            }

            return new NotFoundResult<GetPostResponce>("Error add in Data Base post!");
        }
    }
}
