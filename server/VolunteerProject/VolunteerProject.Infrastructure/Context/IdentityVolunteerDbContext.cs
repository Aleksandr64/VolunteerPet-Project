using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;

namespace VolunteerProject.Infrastructure.Context
{
    public class IdentityVolunteerDbContext : IdentityDbContext<User>
    {
        public IdentityVolunteerDbContext(
            DbContextOptions<IdentityVolunteerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
