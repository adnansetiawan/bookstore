using BookStore.Contracts.BLL;
using BookStore.Contracts.DAL;
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
        private IUnitOfWork _unitOfWork;
        public BookController(IBookBLL bookBLL, IUnitOfWork unitOfWork)
        {
            _bookBLL = bookBLL;
            _unitOfWork = unitOfWork;
        }
        public IHttpActionResult GetAllBook()
        {
            var dtoOutput = _bookBLL.GetAll();
            var response = AutoMapper.Mapper.Map<GetAllBookResponse>(dtoOutput);
            return Ok(response);
        }
    }
}
