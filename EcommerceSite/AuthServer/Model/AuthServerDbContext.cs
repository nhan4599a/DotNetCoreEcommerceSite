using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Model
{
    public class AuthServerDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options) : base(options) { }
    }
}
