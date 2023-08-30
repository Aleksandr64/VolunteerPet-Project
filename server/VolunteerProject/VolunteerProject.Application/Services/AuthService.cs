using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Application.Mappers;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Domain.IdentityModels;
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Context;
using VolunteerProject.Infrastructure.Repositoriy.Interface;
using System.Security.Cryptography;

namespace VolunteerProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepositoriy _authRepositoriy;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthRepositoriy authRepositoriy,
            IConfiguration configuration)
        {
            _authRepositoriy = authRepositoriy;
            _configuration = configuration;
        }

        public async Task<Result<string>> LoginUser(UserLogingRequest userLoging)
        {
            var user = await _authRepositoriy.FindByNameAsync(userLoging.UserName);

            if (user.UserName != userLoging.UserName)
            {
                return new NotFoundResult<string>("There is no user with this Username");
            }

            var userPasswordCheck = VerifyPassword(userLoging.Password, user.PasswordHash, user.PasswordSalt);
            
            if(!userPasswordCheck)
            {
                return new NotFoundResult<string>("This password is not correct");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = GenerateToken(authClaims);

            if(token != null)
            {
                return new SuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return new NotFoundResult<string>("Failed create token");
        }

        public async Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration)
        {
            var userNameCheck = await _authRepositoriy.CheckByNameAsync(userRegistration.UserName);

            if(userNameCheck)
            {
                return new NotFoundResult<string>("A User with such a Username exists");
            }

            var userEmailCheck = await _authRepositoriy.CheckByEmailAsync(userRegistration.Email);

            if(userEmailCheck)
            {
                return new NotFoundResult<string>("A User with such a Email exists");
            }

            var password = HashPaswordCreate(userRegistration.Password);

            var user = userRegistration.ToUser(password);

            var result = await _authRepositoriy.CreateUserAsync(user, UserRolesData.User);

            if (result != null)
            {
                return new SuccessResult<string>(default!);
            }

            return new NotFoundResult<string>("Failer register user");
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private Password HashPaswordCreate(string password)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var salt = RandomNumberGenerator.GetBytes(keySize);
            var bytePassword = Encoding.UTF8.GetBytes(password);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                bytePassword,
                salt,
                iterations,
                hashAlgorithm,
                keySize); 

            return new Password() 
            { 
                hashPassword = Convert.ToHexString(hash),
                saltPassword = Convert.ToHexString(salt)
            };
        }
        private bool VerifyPassword(string password, string hash, string salt)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var saltByte = Convert.FromHexString(salt);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
