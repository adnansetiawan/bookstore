using BookStore.Contracts.BLL;
using BookStore.Contracts.DAL;
using BookStore.Entities.Inputs.Book;
using BookStore.WebApi.Models.Request.Book.Create;
using BookStore.WebApi.Models.Response.Book.Create;
using BookStore.WebApi.Models.Response.Book.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.WebApi.Controllers
{
    [RoutePrefix("api/Book")]
    public class BookController : ApiController
    {
        private IBookBLL _bookBLL;
        public BookController(IBookBLL bookBLL)
        {
            _bookBLL = bookBLL;
        }
        [Authorize]
        public IHttpActionResult GetAllBook()
        {
            var dtoOutput = _bookBLL.GetAll();
            var response = AutoMapper.Mapper.Map<GetAllBookResponse>(dtoOutput);
            return Ok(response);
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult AddNewBook(CreateBookRequest request)
        {
            var response = new CreateBookResponse();
            var dtoInput = AutoMapper.Mapper.Map<CreateNewBookInput>(request);
            try
            {
                _bookBLL.AddNewBook(dtoInput);
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
