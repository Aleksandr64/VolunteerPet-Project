using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Application.DTOs.PostDTOs.Responce
{
    public class GetPostResponce
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlSocialNetwork { get; set; }
        public string UrlFundraisingAccount { get; set; }
        public bool ThisEventHaveEndDate { get; set; } = false;
        public DateTime EndDate { get; set; }
    }
}
