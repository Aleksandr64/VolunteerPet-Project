using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Domain.IdentityModels;

namespace VolunteerProject.Application.Mappers
{
    public static class AuthMapper
    {
        public static Users ToUser(this UserRegistrationRequest userRegister)
        {
            return new Users 
            { 
                LastName = userRegister.LastName,
                FirstName = userRegister.FirstName,
                Email = userRegister.Email,
                UserName = userRegister.UserName,
                PasswordHash = userRegister.Password,
                PhoneNumber = userRegister.PhoneNumber,
            };
        }
    }
}
