using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Application.DTOs.AuthDTOs;
using VolunteerProject.Application.DTOs.AuthDTOs.Request;
using VolunteerProject.Domain.IdentityModels;

namespace VolunteerProject.Application.Mappers
{
    public static class AuthMapper
    {
        public static Users ToUser(this UserRegistrationRequest userRegister, Password password)
        {
            return new Users 
            { 
                LastName = userRegister.LastName,
                FirstName = userRegister.FirstName,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                PasswordHash = password.hashPassword,
                PasswordSalt = password.saltPassword,
                PhoneNumber = userRegister.PhoneNumber,
            };
        }
    }
}
