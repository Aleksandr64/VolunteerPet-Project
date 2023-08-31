using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Application.DTOs.AuthDTOs
{
    public class Password
    {
        public string hashPassword { get; set; } = string.Empty;
        public string saltPassword { get; set; } = string.Empty;
    }
}
