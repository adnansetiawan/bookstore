using BookStore.MVC.Models.ApiResponse.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Services.Contract
{
    public interface ICategoryService
    {
        GetAllCategoryResponse GetAllCategory();
    }
}