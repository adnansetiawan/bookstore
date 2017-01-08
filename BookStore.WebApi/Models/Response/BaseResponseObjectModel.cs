using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebApi.Models.Response
{
    public class BaseResponseObjectModel<T> : BaseMessageResponse
    {
        public T Data { get; set; }
    }
}