using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Repositoriy.Interface
{
    public interface IPostRepositoriy
    {
        public Post CreatePost(Post post);
    }
}
