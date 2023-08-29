using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Domain.IdentityModels
{
    public class User_Role
    {
        public Guid UserId { get; set; }
        public Users ?User { get; set; } 

        public Guid RoleId { get; set; }
        public Roles ?Role { get; set; } 
    }
}
