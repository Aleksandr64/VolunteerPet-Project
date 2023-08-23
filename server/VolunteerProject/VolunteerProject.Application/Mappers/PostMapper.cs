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
        public static Post ToPostAddRequest(this AddPostRequest AddPost)
        {
            return new Post
            {
                Title = AddPost.Title,
                Description = AddPost.Description,
                UrlSocialNetwork = AddPost.UrlSocialNetwork,
                UrlFundraisingAccount = AddPost.UrlFundraisingAccount,
                ThisEventHaveEndDate = AddPost.ThisEventHaveEndDate,    
                EndDate = AddPost.EndDate,
            };
        }
        public static Post ToPostPutRequest(this PutPostRequest PutPost)
        {
            return new Post
            {
                Id = PutPost.Id,
                Title = PutPost.Title,
                Description = PutPost.Description,
                UrlSocialNetwork = PutPost.UrlSocialNetwork,
                UrlFundraisingAccount = PutPost.UrlFundraisingAccount,
                ThisEventHaveEndDate = PutPost.ThisEventHaveEndDate,
                EndDate = PutPost.EndDate,
            };
        }

        public static PostResponce ToPostResponse(this Post post)
        {
            return new PostResponce
            {
                Id = post.Id,   
                Title = post.Title,
                Description = post.Description,
                UrlSocialNetwork = post.UrlSocialNetwork,
                UrlFundraisingAccount = post.UrlFundraisingAccount,
                ThisEventHaveEndDate = post.ThisEventHaveEndDate,
                EndDate = post.EndDate,
            };
        }
    }
}
