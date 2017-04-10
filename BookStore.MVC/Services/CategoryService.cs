using BookStore.MVC.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.MVC.Models.ApiResponse.Category;
using System.Net.Http;

namespace BookStore.MVC.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        protected const string GET_ALL_CATEGORY_URL = "api/Category/GetAll";
        public GetAllCategoryResponse GetAllCategory()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync(GET_ALL_CATEGORY_URL).Result;
            var response = responseMessage.Content.ReadAsAsync<GetAllCategoryResponse>().Result;
            return response;
        }
    }
}