using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.PostDTOs;
using VolunteerProject.Application.DTOs.PostDTOs.Request;
using VolunteerProject.Application.DTOs.PostDTOs.Responce;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Application.Mappers
{
    public static class PostMapper
    {
        public static Post ToPostRequest(this AddPostRequest AddPost)
        {
            return new Post
            {
                Title = AddPost.Title,
                Description = AddPost.Description,
                UrlSocialNetwork = AddPost.UrlSocialNetwork,
                UrlFundraisingAccount = AddPost.UrlFundraisingAccount,
                EndDate = AddPost.EndDate,
            };
        }

        public static GetPostResponce ToPostResponse(this Post post)
        {
            return new GetPostResponce
            {
                Title = post.Title,
                Description = post.Description,
                UrlSocialNetwork = post.UrlSocialNetwork,
                UrlFundraisingAccount = post.UrlFundraisingAccount,
                EndDate = post.EndDate,
            };
        }
    }
}
