using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.Models;
using VolunteerProject.Infrastructure.Context;
using VolunteerProject.Infrastructure.Repositoriy.Interface;

namespace VolunteerProject.Infrastructure.Repositoriy
{
    public class PostRepositoriy : IPostRepositoriy
    {
        private readonly VolunteerDbContext _dbContext;

        public PostRepositoriy(VolunteerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var allPost = await _dbContext.Posts.Include(p => p.User).ToListAsync();
            return allPost;
        }

        public async Task<IEnumerable<Post>> GetPostsByTitle(string title)
        {
            var postByTitle = await _dbContext.Posts.Include(p => p.User)
                .Where(p => p.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
            return postByTitle;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreateDate = DateTime.UtcNow;
            await _dbContext.Posts.AddAsync(post);
            await SaveChanges();
            var postEntity = await _dbContext.Posts.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == post.Id);
            return postEntity;
        }
        public async Task DeletePost(Guid Id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);

            if(post == null)
            {
                return;
            }

            _dbContext.Posts.Remove(post);

            await SaveChanges();
        }
        
        public async Task<Post> ChangePost(Post post)
        {
            _dbContext.Posts.Update(post);

            await SaveChanges();

            var postEntity = await _dbContext.Posts.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == post.Id);

            return postEntity;
            
        }
        public async Task<Users?> GetAllPostsByUserName(string userName)
        {
            var user = await _dbContext.Users.Include(e => e.Posts)
                .FirstOrDefaultAsync(p => p.UserName == userName);
            return user;
        }

        private async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();   
        }
    }
}
