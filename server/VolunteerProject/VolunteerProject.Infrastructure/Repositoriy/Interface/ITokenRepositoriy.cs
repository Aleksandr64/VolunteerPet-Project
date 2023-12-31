﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerProject.Domain.Models;

namespace VolunteerProject.Infrastructure.Repositoriy.Interface
{
    public interface ITokenRepositoriy
    {
        public Task CreateNewLoginAsync(Tokens token);
        public Task<Tokens> FindTokensByNameAsync(string name);
        public Task ChangeDataLogin(Tokens updateUserLogin);

    }
}
