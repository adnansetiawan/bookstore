using AutoMapper;
using BookStore.Contracts.BLL;
using BookStore.Contracts.DAL;
using BookStore.Entities.Inputs.Category;
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
            var response = AutoMapper.Mapper.Map<GetAllCategoryResponse>(dtoOutput);
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult AddCategory(CreateCategoryRequest request)
        {
            var response = new CreateCategoryResponse();
            var dtoInput = AutoMapper.Mapper.Map<CreateNewCategoryInput>(request);
            try
            {
                _categoryBLL.AddNewCategory(dtoInput);
                
               
            }
            catch (Exception ex)
            {
                response.Messages = ex.Message;
                response.Success = false;
            }
             return Ok(response);
        }
    }
        
}
