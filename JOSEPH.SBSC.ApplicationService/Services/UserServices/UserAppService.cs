using JOSEPH.SBSC.ApplicationService.Infrastructure.Extension;
using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Repository.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.ApplicationService.Services.UserServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(User user)
        {
            await _userRepository.AddUser(user);

        }

        public async Task UpdateUserProfile(UserDetailsViewModel user)
        {
            var _user = await _userRepository.GetByApplicationUserId(user.ID, user.ApplicationUserId);
            _user = user.ConvertToUser(_user);

            await _userRepository.UpdateUserProfile(_user);
        }

        public async Task UpdateAppUser(ApplicationUser user)
        {
            await _userRepository.UpdateAppUser(user);
        }

        public async Task ChangeUserStatus(UserListViewModel _user)
        {
            var user = _userRepository.GetUser(_user.ID).FirstOrDefault();
            user.Activated = _user.Activated;
            user.IsApproved = _user.Activated;

            await _userRepository.ChangeUserStatus(user);
        }

        public async Task<LoginResponseData> GetByAppUserId(string AppUserId)
        {
            LoginResponseData user = new LoginResponseData();
            var _user = await _userRepository.GetByAppUserId(AppUserId);

            user.User = _user;
            return user;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var _user = _userRepository.GetUsers()
                .Select(r => new UserListViewModel()
                {
                    ID = r.ID,
                    FullName = r.FirstName + " " + r.LastName,
                    MaritalStatus = r.MaritalStatus,
                    PhoneNumber = r.PhoneNumber,
                    Sex = r.Sex,
                    Email = r.Email,
                    Activated = r.Activated,
                    ApplicationUserId = r.ApplicationUserId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    IsApproved = r.IsApproved,

                }).ToList();

            return _user;
        }

        public async Task<UserDetailsViewModel> GetUser(int UserId)
        {
            var _user = _userRepository.GetUser(UserId)
                .Select(r => new UserDetailsViewModel()
                {
                    ID = r.ID,
                    FullName = r.FirstName + " " + r.LastName,
                    MaritalStatus = r.MaritalStatus,
                    PhoneNumber = r.PhoneNumber,
                    Sex = r.Sex,
                    Email = r.Email,
                    PhoneNumber2 = r.PhoneNumber2,
                    ApplicationUserId = r.ApplicationUserId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Address = r.Address,
                    DateOfBirth = r.DateOfBirth,
                    City = r.City,
                    ProfileImageUrl = r.ProfileImageUrl,
                    Religion = r.Religion,
                }).FirstOrDefault();

            //_user.UserName = await _context.ApplicationUsers.Where(x => x.Id == _user.ApplicationUserId).Select(e => e.UserName).FirstOrDefaultAsync();

            return _user;
        }

        public async Task<User> GetUserDetail(int userId)
        {
            var _user = await _userRepository.GetUserDetail(userId);

            return _user;
        }

        public async Task DeleteFailedUser(User user, ApplicationUser appUser)
        {
            await _userRepository.DeleteFailedUser(user, appUser);
        }
    }
}
