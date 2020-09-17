using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ApplicationUserId { get; set; }
        public string MaritalStatus { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool Activated { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Religion { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
