using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class LoginRequestData
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool RememberMe { get; set; }
    }
}
