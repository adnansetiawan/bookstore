using BookStore.Contracts;
using BookStore.WebApi.Models.Request.User;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        [Route("Login")]
        public async Task<HttpResponseMessage> LoginUser(UserRequestInput user)
        {
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/Token";
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", user.UserName),
                new KeyValuePair<string, string>("password", user.Password)
            };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };
                return responseMsg;
            }
        }
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(UserRequestInput user)
        {


            IdentityResult identityResult = await _userManager.RegisterUser(new Entities.Inputs.User.UserInput
            {
                UserName = user.UserName,
                Password = user.Password
            });

            if (!identityResult.Succeeded)
            {
                //return BadRequest();
            }

            // Auto login after registrаtion (successful user registration should return access_token)
            var loginResult = await this.LoginUser(new UserRequestInput
            {
                UserName = user.UserName,
                Password = user.Password
            });
            return loginResult;

        }
    }

       
}
