using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.IdentityModel
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
