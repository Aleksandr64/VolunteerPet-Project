using Microsoft.AspNetCore.Mvc;
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
    public class AuthRepositoriy : IAuthRepositoriy
    {
        private readonly VolunteerDbContext _dbContext;

        public AuthRepositoriy(VolunteerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> FindByNameAsync(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
            
        }

        public async Task<bool> CheckByNameAsync(string userName)
        {
            var userExist = await _dbContext.Users.AnyAsync(x => x.UserName == userName);
            return userExist;
        }

        public async Task<bool> CheckByEmailAsync(string email)
        {
            var emailExist = await _dbContext.Users.AnyAsync(x => x.Email == email);
            return emailExist;
        }

        public async Task<Users> CreateUserAsync(Users user)
        {
            var result = await _dbContext.Users.AddAsync(user);
            await SaveChanges();
            return result.Entity; 
        }
        private async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
