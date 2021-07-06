using Microsoft.AspNetCore.Identity;
using System;

namespace AuthServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public DateTime? DateOfBirth { get; set; }
    }
}
