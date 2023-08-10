using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Application.Mappers;
using VolunteerProject.Application.Services.Interface;
using VolunteerProject.Domain.ResultModels;

namespace VolunteerProject.Application.Services
{
    internal class AuthService : IAuthService
    {
        public Task<Result<string>> LoginUser(UserLogingRequest userLoging)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> RegisterUser(UserRegistrationRequest userRegistration)
        {
            var user = userRegistration.ToUser();

            if (user == null) 
            {
                return new BadRequestResult<string>("");
            }

            
        }
    }
}
