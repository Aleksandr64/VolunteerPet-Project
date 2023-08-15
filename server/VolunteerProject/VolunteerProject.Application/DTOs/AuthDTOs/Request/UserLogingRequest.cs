using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Application.DTOs.AuthDTOs.Request
{
    public class UserLogingRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
