using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class UserListViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ApplicationUserId { get; set; }
        public string MaritalStatus { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public bool Activated { get; set; }
        public bool IsApproved { get; set; }
        public string Email { get; set; }
    }
}
