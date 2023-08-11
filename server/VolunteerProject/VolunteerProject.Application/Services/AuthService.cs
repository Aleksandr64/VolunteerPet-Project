using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<Result<string>> LoginUser(UserLogingRequest userLoging)
        {
            var user = await _userManager.FindByNameAsync(userLoging.UserName);

            if (user == null)
            {
                return new NotFoundResult<string>("There is no user with this Username");
            }

            var userPasswordCheck = await _userManager.CheckPasswordAsync(user, userLoging.Password); 
            
            if(!userPasswordCheck)
            {
                return new NotFoundResult<string>("This password is not correct");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateToken(authClaims);

            if(token != null)
            {
                return new SuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return new NotFoundResult<string>("Failed create token");
        }

        public async Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration)
        {
            var userNameCheck = await _userManager.FindByNameAsync(userRegistration.UserName);

            if(userNameCheck != null)
            {
                return new NotFoundResult<string>("A User with such a Username exists");
            }

            var userEmailCheck = await _userManager.FindByEmailAsync(userRegistration.Email);

            if(userEmailCheck != null)
            {
                return new NotFoundResult<string>("A User with such a Email exists");
            }

            var user = userRegistration.ToUser();

            if (user == null) 
            {
                return new NotFoundResult<string>("Failled create entity User.");
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

        private JwtSecurityToken GenerateToken(List<Claim> authClaims, bool isRefreshToken = false)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var tokenValidity = isRefreshToken
            //    ? int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"])
            //    : int.Parse(_configuration["Jwt:AccesTokenValidityInMinutes"]);

            //var validity = isRefreshToken
            //    ? DateTime.UtcNow.AddDays(tokenValidity)
            //    : DateTime.UtcNow.AddMinutes(tokenValidity);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
