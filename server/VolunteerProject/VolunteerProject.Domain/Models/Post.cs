using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.IdentityModels;

namespace VolunteerProject.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlSocialNetwork { get; set; } = string.Empty;
        public string UrlFundraisingAccount { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public bool ThisEventHaveEndDate { get; set; } 
        public DateTime EndDate { get; set; }

        public string UserId { get; set; } = string.Empty;
        public Users ?User { get; set; }
    }
}
