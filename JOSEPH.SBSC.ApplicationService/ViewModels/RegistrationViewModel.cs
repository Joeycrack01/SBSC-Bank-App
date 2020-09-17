using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string MaritalStatus { get; set; }
        public string Sex { get; set; }
        public int SalutationId { get; set; }

    }
}
