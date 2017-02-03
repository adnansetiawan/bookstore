using BookStore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Indentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using BookStore.Entities.Inputs.User;
using Microsoft.Owin;
using BookStore.DAL;

namespace BookStore.Services.Authentication
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IdentityFactoryOptions<ApplicationUserManager> options) : base(store)
        {
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
        }

       
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user =  await base.FindAsync(userName, password);
            
            return user;
        }

        public Task<IdentityResult> RegisterUser(UserInput userInput)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userInput.UserName
            };
            return base.CreateAsync(applicationUser, userInput.Password);
        }
    }

}
