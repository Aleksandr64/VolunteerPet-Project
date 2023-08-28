using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Repositoriy.Interface
{
    public interface IPostRepositoriy
    {
        public Task<Post> CreatePost(Post post);
        public Task<IEnumerable<Post>> GetAllPosts();
        public Task<IEnumerable<Post>> GetPostsByTitle(string title);
        public Task DeletePost(Guid Id);
        public Task<Post> ChangePost(Post post);
        public Task<User?> GetAllPostsByUserName(string userName);
    }
}
