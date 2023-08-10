using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Application.Mappers;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Context;

namespace VolunteerProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IdentityVolunteerDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            IdentityVolunteerDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<Result<string>> LoginUser(UserLogingRequest userLoging)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration)
        {
            var userNameCheck = await _userManager.FindByNameAsync(userRegistration.UserName);

            if(userNameCheck != null)
            {
                return new BadRequestResult<string>("A User with such a Username exists");
            }

            var userEmailCheck = await _userManager.FindByEmailAsync(userRegistration.Email);

            if(userEmailCheck != null)
            {
                return new BadRequestResult<string>("A User with such a Email exists");
            }

            var user = userRegistration.ToUser();

            if (user == null) 
            {
                return new BadRequestResult<string>("Failled create entity User.");
            }

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if(!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if(await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            if (result.Succeeded)
            {
                return new SuccessResult<string>(default);
            }

            return new NotFoundResult<string>("Failer register user");
        }
    }
}
