using JOSEPH.SBSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class LoginResponseData
    {
        //public List<PermissionViewModel> AllPermissions { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsStoreAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Issued { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
    }
}
