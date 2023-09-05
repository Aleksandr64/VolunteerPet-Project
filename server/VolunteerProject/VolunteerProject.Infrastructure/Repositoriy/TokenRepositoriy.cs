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
    public class TokenRepositoriy : ITokenRepositoriy
    {
        private readonly VolunteerDbContext _dbContext;

        public TokenRepositoriy(VolunteerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tokens> FindTokensByNameAsync(string userName)
        {
            var user = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }

        public async Task ChangeDataLogin(Tokens updateUserLogin)
        {
            _dbContext.Tokens.Update(updateUserLogin);
            await SaveChanges();
        }

        public async Task CreateNewLoginAsync(Tokens token)
        {
            await _dbContext.Tokens.AddAsync(token);
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
