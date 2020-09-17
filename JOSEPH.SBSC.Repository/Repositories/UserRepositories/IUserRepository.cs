using JOSEPH.SBSC.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task ChangeUserStatus(User _user);
        Task DeleteFailedUser(User user, ApplicationUser appUser);
        Task<User> GetByApplicationUserId(int userId, string AppUserId);
        Task<User> GetByAppUserId(string AppUserId);
        IQueryable<User> GetUser(int UserId);
        Task<User> GetUserDetail(int UserId);
        IEnumerable<User> GetUsers();
        Task UpdateAppUser(ApplicationUser user);
        Task UpdateUserProfile(User user);
    }
}