using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Repositoriy.Interface;

namespace VolunteerProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoriy _userRepositoriy;

        public UserService(IUserRepositoriy userRepositoriy)
        {
            _userRepositoriy = userRepositoriy;
        }

        public async Task<Result<IEnumerable<string>>> GetUserNameByTitle(string userName)
        {
            var usersByUserName = await _userRepositoriy.GetUsersByUserName(userName);

            if(userName == null)
            {
                return new NotFoundResult<IEnumerable<string>>("Failed responce data base!");
            }

            var userNameList = usersByUserName.Select(x => x.UserName).ToList();

            return new SuccessResult<IEnumerable<string>>(userNameList!);
        }
    }
}
