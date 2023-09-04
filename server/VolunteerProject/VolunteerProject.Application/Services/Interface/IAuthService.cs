
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Domain.ResultModels;

namespace VolunteerProject.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<Result<TokenResponce>> LoginUser(UserLogingRequest userLoging);
        Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration);
        Task<Result<TokenResponce>> GetNewAccessToken(TokenRequest token);
        Task<Result<string>> Logout(string accessToken);
    }
}
