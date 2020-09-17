using JOSEPH.SBSC.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using JOSEPH.SBSC.Core.Utilities;

namespace JOSEPH.SBSC.API.Controllers
{
    public class BaseApiController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public int UserId;

        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public int GetUserId()
        {
            UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);
            return UserId;
        }
        

        [ApiExplorerSettings(IgnoreApi = true)]
        public void LogError(Exception ex)
        {
            var UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    UserId = UserId,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };

                // implement error logging here
            }
            catch { }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string Error()
        {
            return "Oops! we encountered some difficulties. please contact us at support@expressgrocrey.com";
        }
    }
}
