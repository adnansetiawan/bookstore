using AutoMapper;
using BookStore.Contracts.BLL;
using BookStore.WebApi.Models.Request.Category;
using BookStore.WebApi.Models.Request.Category.Create;
using BookStore.WebApi.Models.Response.Category;
using BookStore.WebApi.Models.Response.Category.Create;
using BookStore.WebApi.Models.Response.Category.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
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

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult AddCategory(CreateCategoryRequest request)
        {

            var dtoInput = Mapper.Map<DtoInput.CategoryDto>(request);
            _categoryBLL.AddNewCategory(dtoInput);
            var response = new AddCategoryResponse();           
            return Ok(response);
        }
    }
        
}
