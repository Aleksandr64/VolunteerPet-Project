using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Domain.Models
{
    public class Post : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlSocialNetwork { get; set; } = string.Empty;
        public string UrlFundraisingAccount { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool ThisEventHaveEndDate { get; set; } 
        public DateTime EndDate { get; set; }

        public Guid UserId { get; set; } 
        public Users? User { get; set; }
    }
}
