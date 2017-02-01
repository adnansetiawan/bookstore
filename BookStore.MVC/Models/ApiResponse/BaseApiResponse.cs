using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models.ApiResponse
{
    public abstract class BaseApiResponse
    {
        public bool Success { get; set; }
        public string Messages { get; set; }
      
    }
}