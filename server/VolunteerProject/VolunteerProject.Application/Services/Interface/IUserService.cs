using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.ResultModels;

namespace VolunteerProject.Application.Services.Interface
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<string>>> GetUserNameByTitle(string userName);
    }
}
