
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
        Task<Result<string>> LoginUser(UserLogingRequest userLoging);
        Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration);
    }
}
