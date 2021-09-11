using System;
using Microsoft.AspNetCore.Identity;

namespace IMDBClone.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}