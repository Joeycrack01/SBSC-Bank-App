using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.Infrastructure.Extension
{
    public static class UserExtension
    {
        public static User ConvertToUser(this UserDetailsViewModel vm, User user)
        {
            user.MaritalStatus = vm.MaritalStatus;
            user.PhoneNumber = vm.PhoneNumber;
            user.Sex = vm.Sex;
            user.Email = vm.Email;
            user.PhoneNumber2 = vm.PhoneNumber2;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.Address = vm.Address;
            user.DateOfBirth = vm.DateOfBirth;
            user.City = vm.City;
            user.ProfileImageUrl = vm.ProfileImageUrl;
            user.Religion = vm.Religion;

            return user;
        }
    }
}
