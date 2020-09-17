using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;

        }

        public async Task AddUser(User user)
        {
            if (user.ID > 0)
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                var _user = await _context.User.Where(u => u.ApplicationUserId == user.ApplicationUserId).FirstOrDefaultAsync();

            }

        }

        public async Task UpdateUserProfile(User user)
        {
            _context.User.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByApplicationUserId(int userId, string AppUserId)
        {
            var _user = await _context.User.Where(k => k.ID == userId && k.ApplicationUserId == AppUserId).FirstOrDefaultAsync();
            return _user;
        }

        public async Task UpdateAppUser(ApplicationUser user)
        {
            var _user = await _context.ApplicationUsers.Where(k => k.Id == user.Id && k.Email == user.Email).FirstOrDefaultAsync();

            _user.PasswordResetToken = user.PasswordResetToken;
            _context.ApplicationUsers.Update(_user);

            await _context.SaveChangesAsync();
        }

        public async Task ChangeUserStatus(User _user)
        {
            _context.User.Update(_user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByAppUserId(string AppUserId)
        {
            var _user = _context.User.Where(e => e.ApplicationUserId == AppUserId).FirstOrDefault();

            return _user;
        }

        public IEnumerable<User> GetUsers()
        {
            var _user = _context.User.AsEnumerable();

            return _user;
        }

        public IQueryable<User> GetUser(int UserId)
        {
            var _user = _context.User.Where(e => e.ID == UserId);

            return _user;
        }

        public async Task<User> GetUserDetail(int UserId)
        {
            var _user = await _context.User.Where(e => e.ID == UserId).FirstOrDefaultAsync();

            return _user;
        }

        public async Task DeleteFailedUser(User user, ApplicationUser appUser)
        {
            _context.User.Remove(user);
            _context.Users.Remove(appUser);

            await _context.SaveChangesAsync();
        }

    }
}
