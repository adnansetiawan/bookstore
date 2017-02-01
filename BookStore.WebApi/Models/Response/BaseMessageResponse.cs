using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebApi.Models.Response
{
    public abstract class BaseMessageResponse
    {
        public bool Success { get; set; }
        public string Messages { get; set; }
        public BaseMessageResponse()
        {
            Success = true;
            Messages = "Success";
            
        }
    }
}