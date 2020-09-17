using JOSEPH.SBSC.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOSEPH.SBSC.Repository.Repositories.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;

        }

        public IdentityUser Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;


            var user = _context.ApplicationUsers.Where(x => x.UserName == username).FirstOrDefault();

            // check if username exist
            if (user == null)
            {
                throw new Exception("A user with this username/password does not exit");
            }

            //authentication successful
            return user;
        }

        public int checkUserName(string username)
        {

            var user = _context.ApplicationUsers.Where(x => x.NormalizedUserName == username.ToUpper()).Count();

            return user;
        }

        public string checkUserNameById(string username)
        {

            var user = _context.ApplicationUsers.Where(x => x.NormalizedUserName == username.ToUpper()).Select(r => r.Id).FirstOrDefault();

            return user;
        }
    }



    public interface IAccountRepository
    {
        int checkUserName(string username);
        IdentityUser Authenticate(string username, string password);
        string checkUserNameById(string username);
    }
}
