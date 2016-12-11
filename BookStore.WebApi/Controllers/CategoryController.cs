using AutoMapper;
using BookStore.Contracts.BLL;
using BookStore.WebApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.WebApi.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private ICategoryBLL _categoryBLL;

        public CategoryController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }
        public IHttpActionResult GetAllCategory()
        {
            var dtoOutput = _categoryBLL.GetAll();
            var categoryModel = Mapper.Map<List<CategoryResponse>>(dtoOutput);
            var response = new GetAllCategoryResponse
            {
                Data = categoryModel

            };
            return Ok(response);
        }
    }
        
}
