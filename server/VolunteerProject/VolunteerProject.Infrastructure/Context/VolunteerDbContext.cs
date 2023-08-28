using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Context
{
    public class VolunteerDbContext : IdentityDbContext<User>
    {
        public VolunteerDbContext(
            DbContextOptions<VolunteerDbContext> options) 
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Ignore<IdentityUserClaim<string>>();
            //modelBuilder.Ignore<IdentityUserLogin<string>>();
            //modelBuilder.Ignore<IdentityRoleClaim<string>>();
        }

        public DbSet<Post> Posts { get; set; }
    }
}
