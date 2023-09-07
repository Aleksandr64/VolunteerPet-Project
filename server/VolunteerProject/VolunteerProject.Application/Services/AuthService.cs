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
using VolunteerProject.Domain.ResultModels;
using VolunteerProject.Infrastructure.Context;
using VolunteerProject.Infrastructure.Repositoriy.Interface;
using System.Security.Cryptography;
using VolunteerProject.Domain.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace VolunteerProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositoriy _authRepositoriy;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepositoriy _tokenRepositoriy;

        public AuthService(
            IUserRepositoriy authRepositoriy,
            IConfiguration configuration,
            ITokenRepositoriy tokenRepositoriy)
        {
            _authRepositoriy = authRepositoriy;
            _configuration = configuration;
            _tokenRepositoriy = tokenRepositoriy;
        }

        public async Task<Result<TokenResponce>> LoginUser(UserLogingRequest userLoging)
        {
            var user = await _authRepositoriy.FindByNameAsync(userLoging.UserName);

            if (user.UserName != userLoging.UserName)
            {
                return new NotFoundResult<TokenResponce>("There is no user with this Username");
            }

            var userPasswordCheck = VerifyPassword(userLoging.Password, user.PasswordHash, user.PasswordSalt);

            if (!userPasswordCheck)
            {
                return new NotFoundResult<TokenResponce>("This password is not correct");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRolesEnum), user.Role))
            };

            var accessToken = GenerateAccessToken(authClaims);
            var refreshToken = GenerateRefreshToken();

            var userRefreshToken = await _tokenRepositoriy.FindTokensByNameAsync(user.UserName);

            userRefreshToken.RefreshToken = refreshToken;
            userRefreshToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _tokenRepositoriy.ChangeDataLogin(userRefreshToken);

            return new SuccessResult<TokenResponce>(new TokenResponce
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration)
        {
            var userNameCheck = await _authRepositoriy.CheckByNameAsync(userRegistration.UserName);

            if (userNameCheck)
            {
                return new NotFoundResult<string>("A User with such a Username exists");
            }

            var userEmailCheck = await _authRepositoriy.CheckByEmailAsync(userRegistration.Email);

            if (userEmailCheck)
            {
                return new NotFoundResult<string>("A User with such a Email exists");
            }

            var password = HashPaswordCreate(userRegistration.Password);

            var user = userRegistration.ToUser(password, UserRolesEnum.User);

            var result = await _authRepositoriy.CreateUserAsync(user);

            await _tokenRepositoriy.CreateNewLoginAsync(new Tokens 
            { 
                UserName = result.UserName
            });

            if (result != null)
            {
                return new SuccessResult<string>(default!);
            }

            return new NotFoundResult<string>("Failer register user");
        }

        public async Task<Result<TokenResponce>> GetNewAccessToken(TokenRequest token)
        {
            string accessToken = token.AccessToken;
            string refreshToken = token.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return new NotFoundResult<TokenResponce>("Invalid Token");
            }

            var userName = principal.Identity.Name;

            var user = await _tokenRepositoriy.FindTokensByNameAsync(userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new NotFoundResult<TokenResponce>("Refresh token invalid or the token has expired");
            }

            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); 

            await _tokenRepositoriy.ChangeDataLogin(user);

            return new SuccessResult<TokenResponce>(new TokenResponce
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }

        public async Task<Result<string>> Logout(string accessToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return new NotFoundResult<string>("Invalid Token");
            }

            var userName = principal.Identity.Name;

            var user = await _tokenRepositoriy.FindTokensByNameAsync(userName);

            user.RefreshToken = string.Empty;
            user.RefreshTokenExpiryTime = default;

            await _tokenRepositoriy.ChangeDataLogin(user);

            return new SuccessResult<string>(default!);

        }

        private string GenerateAccessToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParametrs = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParametrs, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return default!;
            }

            return principal;
        }

        public string ValidateToken(string accessToken)
        {
            if (accessToken == null)
            {
                return default!;
            }

            var tokenHandler = new JwtSecurityTokenHandler();


            var tokenValidationParametrs = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParametrs, out validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;

            if (jwtToken == null)
            {
                return default!;
            }

            var roleUser = principal.FindFirstValue(ClaimTypes.Role);

            return roleUser;
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

            var hashPassword = CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));

            return hashPassword;
        }
    }
}
