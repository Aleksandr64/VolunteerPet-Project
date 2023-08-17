using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Application.DTOs.PostDTOs.Request
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlSocialNetwork { get; set; }
        public string UrlFundraisingAccount { get; set; }
        public DateTime EndDate { get; set; }
    }
}
