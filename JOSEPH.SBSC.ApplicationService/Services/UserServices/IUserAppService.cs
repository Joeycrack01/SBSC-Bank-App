using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.ApplicationService.Services.UserServices
{
    public interface IUserAppService
    {
        Task AddUser(User user);
        Task ChangeUserStatus(UserListViewModel _user);
        Task DeleteFailedUser(User user, ApplicationUser appUser);
        Task<LoginResponseData> GetByAppUserId(string AppUserId);
        Task<UserDetailsViewModel> GetUser(int UserId);
        Task<User> GetUserDetail(int userId);
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task UpdateAppUser(ApplicationUser user);
        Task UpdateUserProfile(UserDetailsViewModel user);
    }
}