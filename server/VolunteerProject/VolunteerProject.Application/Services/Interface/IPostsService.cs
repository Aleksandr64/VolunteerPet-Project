using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.PostDTOs;
using VolunteerProject.Application.DTOs.PostDTOs.Request;
using VolunteerProject.Application.DTOs.PostDTOs.Responce;
using VolunteerProject.Domain.ResultModels;

namespace VolunteerProject.Application.Services.Interface
{
    public interface IPostsService
    {
        public Task<Result<GetPostResponce>> AddPost(AddPostRequest postData);
        public Task<Result<IEnumerable<GetPostResponce>>> GetAllPosts();
        public Task<Result<IEnumerable<GetPostResponce>>> GetPostByTitle(string title);
    }
}
