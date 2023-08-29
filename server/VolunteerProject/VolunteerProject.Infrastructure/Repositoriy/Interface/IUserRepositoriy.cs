using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;

namespace VolunteerProject.Infrastructure.Repositoriy.Interface
{
    public interface IUserRepositoriy
    {
        public Task<IEnumerable<Users>> GetUsersByUserName(string userName);
    }
}
