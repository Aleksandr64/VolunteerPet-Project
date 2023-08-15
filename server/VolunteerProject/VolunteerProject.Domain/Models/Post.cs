using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerProject.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlSocialNetwork { get; set; }
        public string UrlFundraisingAccount { get; set; }
    }
}
