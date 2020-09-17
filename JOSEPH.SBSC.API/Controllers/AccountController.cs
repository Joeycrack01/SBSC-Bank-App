using JOSEPH.SBSC.API.Helpers;
using JOSEPH.SBSC.ApplicationService.Services.UserServices;
using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Core.Utilities;
using JOSEPH.SBSC.Repository.Repositories.AccountRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserAppService _userRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        public AccountController(
            IHttpContextAccessor httpContextAccessor,
            IJwtFactory jwtFactory,
            IAccountRepository accountRepository,
            IUserAppService userRepo,
            IOptions<JwtIssuerOptions> jwtOptions, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager) 
            : base(httpContextAccessor)
        {
            _accountRepo = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [AllowAnonymous]
        [HttpPost, Route("SignUp")]
        public async Task<ResponseBase> SignUp([FromBody] RegistrationViewModel userDetails)
        {
            var checkUserName = _accountRepo.checkUserName(userDetails.Email);

            if (checkUserName > 0)
            {
                return new ResponseBase() { ErrorDetails = "Email Already taken by another user", IsSuccess = false };
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = userDetails.Email,
                Email = userDetails.Email,
                PhoneNumber = userDetails.PhoneNumber,

            };

            User newUser = new User()
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                PhoneNumber = userDetails.PhoneNumber,
                Sex = userDetails.Sex,
                ApplicationUserId = user.Id,
                IsApproved = false,
                Activated = true,
                Email = userDetails.Email,
            };


            try
            {
                var result = await _userManager.CreateAsync(user, userDetails.Password);

                if (!result.Succeeded)
                {

                    return new ResponseBase() { ErrorDetails = "Make your password stronger and try again ", IsSuccess = false };
                }
                else
                {

                    try
                    {
                        await _userRepo.AddUser(newUser);

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { UserId = user.Id, Code = code }, protocol: HttpContext.Request.Scheme);

                        // send email confirmation mail here

                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("UserName", user.UserName));
                        
                        await _signInManager.SignOutAsync();
                        user.LockoutEnabled = false;
                        user.EmailConfirmed = false;

                        return new ResponseBase() { IsSuccess = true };
                    }
                    catch (Exception ex)
                    {
                        await _userRepo.DeleteFailedUser(newUser, user);
                        return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false };

                    }
                }
            }
            catch (Exception ex)
            {
                
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false };
            }

        }

        [AllowAnonymous]
        [HttpGet, Route("ConfirmEmail")]
        public ResponseBase<string> ConfirmEmail(string userId, string code)
        {

            var user = _userManager.FindByIdAsync(userId).Result;
            IdentityResult result = _userManager.ConfirmEmailAsync(user, code).Result;

            if (result.Succeeded)
            {
                return new ResponseBase<string>() { Payload = "Your Email has been successfully verified", IsSuccess = true };
            }
            else
            {
                return new ResponseBase<string>() { Payload = "Your Email has not been verified please contact the admin", IsSuccess = false };
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("ForgotPassword")]
        public async Task<ResponseBase<string>> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user == null)
                {
                    return new ResponseBase<string>() { ErrorDetails = "Email does not exist! make sure to input correct email address", IsSuccess = false, Payload = null };
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                user.PasswordResetToken = code;

                await _userRepo.UpdateAppUser(user);

                var ChangePasswordUrl = _httpContextAccessor.HttpContext.Request.Scheme
                      + "://" + _httpContextAccessor.HttpContext.Request.Host.Host + ":" + _httpContextAccessor.HttpContext.Request.Host.Port + "/reset-password";

                // send password reset mail here
               
                return new ResponseBase<string>() { Payload = "password reset instruction has been sent to your mail", IsSuccess = true };

            }
            catch (Exception ex)
            {
                //LogError(ex);
                return new ResponseBase<string>() { ErrorDetails = "there was an error processing your forgot password request", IsSuccess = false, Payload = null };
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("ResetPassword")]
        public async Task<ResponseBase<string>> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            try
            {

                var user = _userManager.FindByEmailAsync(model.Email).Result;



                IdentityResult result = _userManager.ResetPasswordAsync(user, user.PasswordResetToken, model.Password).Result;

                if (result.Succeeded)
                {
                    user.PasswordResetToken = "";
                    await _userRepo.UpdateAppUser(user);
                    return new ResponseBase<string>() { Payload = "Your password reset was successful", IsSuccess = true };
                }
                else
                {
                    return new ResponseBase<string>() { Payload = "Your password reset failed contact the admin", IsSuccess = false };
                }

            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<string>() { Payload = "Your password reset failed contact the admin", IsSuccess = false };

            }
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<ResponseBase<LoginResponseData>> Login([FromBody]LoginRequestData model)
        {
            try
            {

                await _signInManager.SignOutAsync();

                var lrd = new LoginResponseData();
                ApplicationUser user = await _userManager.FindByNameAsync(model.username);
                if (user == null)
                {
                    return new ResponseBase<LoginResponseData>() { ErrorDetails = "Login details entered is incorrect", Payload = null };
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.password, false, false);
                var _user = await _userRepo.GetByAppUserId(user.Id);

                // BUG: This shouldn't allow login if email still requires verification
                if (result.Succeeded)
                {

                    var identity = await GetClaimsIdentity(model.username, model.password);
                    if (identity == null)
                    {
                        return new ResponseBase<LoginResponseData>() { ErrorDetails = "Login details entered is incorrect", Payload = null };
                    }

                    var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                    Token Token = JsonConvert.DeserializeObject<Token>(jwt);
                    _user.Token = Token.auth_Token;

                    if (result.RequiresTwoFactor)
                    {
                        //return RedirectToPage("./SendCode", new { returnUrl, RememberMe });
                    }
                    if (result.IsLockedOut)
                    {

                        return new ResponseBase<LoginResponseData>() { ErrorDetails = "This you have been LockedOut until " + user.LockoutEnd, Payload = null };
                    }

                    if (_user.User.Activated != true)
                    {
                        return new ResponseBase<LoginResponseData>() { ErrorDetails = "Your  account has been deactivated contact the admin", Payload = null };
                    }

                }
                else
                {
                    return new ResponseBase<LoginResponseData>() { ErrorDetails = "Login details entered is incorrect", Payload = null };
                }
                
                return new ResponseBase<LoginResponseData>() { Payload = _user, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResponseBase<LoginResponseData>() { ErrorDetails = Error(), Payload = null };
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("Logout")]
        public async Task<ResponseBase> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new ResponseBase() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { ErrorDetails = "Oops! Request did not complete Successfully", IsSuccess = false };
            }

        }

        [Authorize]
        [HttpGet, Route("GetUsers")]
        public async Task<ResponseBase<IEnumerable<UserListViewModel>>> GetUsers()
        {
            try
            {
                var _users = await _userRepo.GetUsers();

                return new ResponseBase<IEnumerable<UserListViewModel>>() { Payload = _users, IsSuccess = true };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<UserListViewModel>>() { ErrorDetails = Error(), Payload = null };
            }
        }

        [Authorize]
        [HttpGet, Route("GetUserProfile/{UserId}")]
        public async Task<ResponseBase<UserDetailsViewModel>> GetUserProfile(int UserId)
        {
            try
            {
                var _users = await _userRepo.GetUser(UserId);

                return new ResponseBase<UserDetailsViewModel>() { Payload = _users, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResponseBase<UserDetailsViewModel>() { ErrorDetails = ex.Message, Payload = null };
            }
        }

        [Authorize]
        [HttpPost, Route("UpdateUserProfile")]
        public async Task<ResponseBase> UpdateUserProfile([FromBody]UserDetailsViewModel model)
        {
            try
            {
                var checkUserName = _accountRepo.checkUserNameById(model.UserName);

                if (checkUserName != model.ApplicationUserId)
                {
                    return new ResponseBase() { ErrorDetails = "UserName Already taken by another user", IsSuccess = false };
                }

                ApplicationUser user = await _userManager.FindByIdAsync(model.ApplicationUserId);

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {

                    return new ResponseBase() { ErrorDetails = "There was an error updating your profile contact the admin ", IsSuccess = false };
                }
                else
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("UserName", user.UserName));

                    await _userRepo.UpdateUserProfile(model);
                    return new ResponseBase() { IsSuccess = true, IsUpdate = true };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase() { ErrorDetails = ex.Message };
            }

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var user = await _userRepo.GetByAppUserId(userToVerify.Id);

                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, user.User.ID.ToString()));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

    }
}
