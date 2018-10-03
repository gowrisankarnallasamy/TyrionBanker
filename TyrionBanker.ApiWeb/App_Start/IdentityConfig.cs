using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TyrionBanker.ApiWeb.Models;
using Unity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using TyrionBanker.Domain.ApplicationService;
using TyrionBanker.Domain.Models;
using System.Collections.Generic;
using TyrionBanker.Core.Log;
using System.Security.Claims;
using System.Linq;
using TyrionBanker.Core;

namespace TyrionBanker.ApiWeb
{

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    /// <summary>
    /// UserStore.
    /// </summary>
    public class UserStore : IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser, string>,
        IRoleStore<ApplicationRole, string>,
        IUserRoleStore<ApplicationUser, string>
    {
        private static IUnityContainer Container => UnityConfig.Container;

        #region User
        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ApplicationUser Find By Id.
        /// </summary>
        /// <param name="userId">Id</param>
        /// <returns>ApplicationUser</returns>
        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var result = new ApplicationUser() { Id = userId };
            return Task.FromResult(result);
        }

        /// <summary>
        /// ApplicationUser Find By Name.
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>ApplicationUser</returns>
        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            UserInfoDomain user;
            try
            {
                IUserManagementApplicationService Service = Container.Resolve<IUserManagementApplicationService>();
                user = Service.GetUserInfo(userName);

                if (user == null)
                {
                    return Task.FromResult<ApplicationUser>(null);
                }
                var result = new ApplicationUser()
                {
                    Id = user.UserId.ToString(),
                    UserId = user.UserId,
                    Name = user.Name,
                    PasswordHash = user.Password,
                    UserName = user.Name,
                };

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(new ApplicationUser());
        }

        /// <summary>
        /// Get Password Hash.
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>Password Hash</returns>
        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Role

        public Task CreateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationRole> IRoleStore<ApplicationRole, string>.FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationRole> IRoleStore<ApplicationRole, string>.FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Roles.
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>Roles</returns>
        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            IList<string> result;
            IUserManagementApplicationService Service = Container.Resolve<IUserManagementApplicationService>();
            result = Service.GetRoles(int.Parse(user.Id));

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Dispose Resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserStore()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose Resources.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }


    /// <summary>
    /// UserManager.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private static IUnityContainer Container => UnityConfig.Container;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="store">UserStore</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        /// <summary>
        /// Check Password.
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="password">Password</param>
        /// <returns>IsValid</returns>
        public override async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var valid = PassworHelper.ValidatePassword(password, user.PasswordHash);
            return await Task.FromResult(valid);
        }

        /// <summary>
        /// Create Identity.
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="authenticationType">AuthenticationType</param>
        /// <returns>Identity</returns>
        public override async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            // Set UserAccount
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            // Set Role Function
            IUserManagementApplicationService Service = Container.Resolve<IUserManagementApplicationService>();
            var functions = Service.GetRoleFunctions(int.Parse(user.Id));
            identity.AddClaims(functions.Select(f => new Claim("function", f.FunctionName)));
            return identity;
        }
    }

    /// <summary>
    /// RoleManager.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="store">RoleStore</param>
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }
    }

    /// <summary>
    /// SignInManager.
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        private static IUnityContainer Container => UnityConfig.Container;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userManager">ApplicationUserManager</param>
        /// <param name="authenticationManager">AuthenticationManager</param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Password SignIn.
        /// </summary>
        /// <param name="account">User Account</param>
        /// <param name="password">Password</param>
        /// <param name="isPersistent">IsPersistent</param>
        /// <param name="shouldLockout">Should Lockout</param>
        /// <returns>SignInStatus</returns>
        public override async Task<SignInStatus> PasswordSignInAsync(string account, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await UserManager.FindAsync(account, password);
            if (user == null) return SignInStatus.Failure;


            //Todo
            /*if (!Container.Resolve<Organization>().IsGlobalAdmin && !user.HasRoles && !user.HasGroups) return SignInStatus.RequiresVerification;

            if (user.ExpirationDate.HasValue && user.ExpirationDate.Value < DateTime.UtcNow)
            {
                if (!Container.Resolve<Organization>().IsGlobalAdmin)
                {
                    IAuthenticationApplicationService Service = Container.Resolve<IAuthenticationApplicationService>();
                    // Event Log
                    Service.LoginFailed(user.UserId, user.UserAccount);
                }
                return SignInStatus.LockedOut;
            }

            await SignInAsync(user, isPersistent, true);

            // Set UserInfo To UnityContext
            AuthenticationAttribute.SetUserInfoToUnityContext(user.UserId, user.Name, user.UserName);*/

            return SignInStatus.Success;
        }

        /// <summary>
        /// SignOut.
        /// </summary>
        public void SignOut()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
