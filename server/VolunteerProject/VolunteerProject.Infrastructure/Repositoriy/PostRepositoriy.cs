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

        public Post CreatePost(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreateDate = DateTime.UtcNow;
            var postEntity = _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return postEntity.Entity;
        }
    }
}
