using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.Data
{
    public class AuthContext : IdentityDbContext<Security.ApplicationUser>
    {
        public AuthContext(DbContextOptions option) : base(option)
        {
        }


    }
}
