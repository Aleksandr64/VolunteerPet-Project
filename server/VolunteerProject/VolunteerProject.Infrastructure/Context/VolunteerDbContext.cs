using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Context
{
    public class VolunteerDbContext : DbContext
    {
        public VolunteerDbContext(
            DbContextOptions<VolunteerDbContext> options) 
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
