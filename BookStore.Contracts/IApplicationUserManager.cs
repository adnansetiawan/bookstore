using BookStore.Entities.Indentity;
using BookStore.Entities.Inputs.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts
{
    public interface IApplicationUserManager
    {
        Task<IdentityResult> RegisterUser(UserInput user);
        Task<IdentityUser> FindUser(string userName, string password);
    }
}
