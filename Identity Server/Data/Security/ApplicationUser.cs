using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.Data.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { set; get; }
    }
}
