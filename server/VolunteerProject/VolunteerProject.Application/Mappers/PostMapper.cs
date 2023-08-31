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
        public static Post ToPostAddRequest(this AddPostRequest addPost, Users user)
        {
            return new Post
            {
                Title = addPost.Title,
                Description = addPost.Description,
                UrlSocialNetwork = addPost.UrlSocialNetwork,
                UrlFundraisingAccount = addPost.UrlFundraisingAccount,
                ThisEventHaveEndDate = addPost.ThisEventHaveEndDate,    
                EndDate = addPost.EndDate,
                UserId = user.Id
            };
        }
        public static Post ToPostPutRequest(this PutPostRequest putPost, Users user)
        {
            return new Post
            {
                Id = putPost.Id,
                Title = putPost.Title,
                Description = putPost.Description,
                UrlSocialNetwork = putPost.UrlSocialNetwork,
                UrlFundraisingAccount = putPost.UrlFundraisingAccount,
                ThisEventHaveEndDate = putPost.ThisEventHaveEndDate,
                EndDate = putPost.EndDate,
                UserId = user.Id
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
                UserName = post.User.UserName,
            };
        }
    }
}
