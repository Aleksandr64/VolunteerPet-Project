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
        public static Users ToUser(this UserRegistrationRequest userRegister, string hashPassword, string saltPassword)
        {
            return new Users 
            { 
                LastName = userRegister.LastName,
                FirstName = userRegister.FirstName,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                PasswordHash = hashPassword,
                PasswordSalt = saltPassword,
                PhoneNumber = userRegister.PhoneNumber,
            };
        }
    }
}
