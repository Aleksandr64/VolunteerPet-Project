using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Repositoriy.Interface
{
    public interface IUserRepositoriy
    {
        public Task<Users> FindByNameAsync(string name);
        public Task<bool> CheckByNameAsync(string name);
        public Task<bool> CheckByEmailAsync(string email);
        public Task<Users> CreateUserAsync(Users user);
        public Task<IEnumerable<Users>> GetUsersByUserName(string userName);
    }
}
