using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebApi.Models.Request.User
{
    public class UserRequestInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}