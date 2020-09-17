using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string PasswordResetToken { get; set; }
    }
}
