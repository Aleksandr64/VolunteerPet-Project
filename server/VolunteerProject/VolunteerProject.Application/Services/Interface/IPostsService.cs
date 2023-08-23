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
        public Task<Result<PostResponce>> AddPost(AddPostRequest postData);
        public Task<Result<IEnumerable<PostResponce>>> GetAllPosts();
        public Task<Result<IEnumerable<PostResponce>>> GetPostByTitle(string title);
        public Task<Result<string>> DeletePost(Guid Id);
        public Task<Result<PostResponce>> ChangeOfDataPost(PutPostRequest changePost);
    }
}
