using BookStore.Contracts;
using BookStore.WebApi.Models.Request.User;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        IApplicationUserManager _userManager;
        public AccountController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
      
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserRequestInput user)
        {
            

            IdentityResult result = await _userManager.RegisterUser(new Entities.Inputs.User.UserInput
            {
                UserName = user.UserName,
                Password = user.Password
            });

            
            return Ok();
        }
    }
}
