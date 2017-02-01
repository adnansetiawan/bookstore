using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models.ApiResponse
{
    public class BaseApiResponseObjectModel<T> : BaseApiResponse
    {
        public T Data { get; set; }
    }
}