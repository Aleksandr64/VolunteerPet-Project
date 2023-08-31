using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Application.DTOs.PostDTOs.Request
{
    public class PutPostRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlSocialNetwork { get; set; } = string.Empty;
        public string UrlFundraisingAccount { get; set; } = string.Empty;
        public bool ThisEventHaveEndDate { get; set; }
        public DateTime EndDate { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
