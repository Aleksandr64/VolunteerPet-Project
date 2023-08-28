using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Infrastructure.Context;
using VolunteerProject.Infrastructure.Repositoriy.Interface;

namespace VolunteerProject.Infrastructure.Repositoriy
{
    public class UserRepositoriy : IUserRepositoriy
    {
        private readonly VolunteerDbContext _dbContext;

        public UserRepositoriy(VolunteerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<User>> GetUsersByUserName(string userName)
        {
            var allUserByUserName = await _dbContext.Users
                .Where(p => p.UserName.Contains(userName))
                .ToListAsync();

            return allUserByUserName;
        }
    }
}
