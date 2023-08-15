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
        public static User ToUser(this UserRegistrationRequest userRegister)
        {
            return new User 
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
