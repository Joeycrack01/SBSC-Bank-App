using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class UserDetailsViewModel
    {
        public int ID { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ApplicationUserId { get; set; }
        public string MaritalStatus { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Religion { get; set; }
        public string UserName { get; set; }
    }
}
