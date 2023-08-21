using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var allPost = await _dbContext.Posts.ToListAsync();
            return allPost;
        }

        public async Task<IEnumerable<Post>> GetPostsByTitle(string title)
        {
            var postByTitle = await _dbContext.Posts.Where(p => p.Title.ToLower().Contains(title.ToLower())).ToListAsync();
            return postByTitle;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreateDate = DateTime.UtcNow;
            var postEntity = await _dbContext.Posts.AddAsync(post);
            await SaveChanges();
            return postEntity.Entity;
        }
        public async Task<string> DeletePost(Guid Id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);
            if(post == null)
            {
                return default!;
            }
            _dbContext.Posts.Remove(post);
            await SaveChanges();
            return "Succeed delete publication";
        }

        private async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();   
        }
    }
}
