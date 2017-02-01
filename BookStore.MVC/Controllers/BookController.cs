using BookStore.MVC.Models.ApiResponse.Book;
using BookStore.MVC.Models.ViewModel.Book;
using BookStore.MVC.Models.ViewModel.Book.GetAll;
using BookStore.MVC.Models.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookStore.MVC.Controllers
{
    public class BookController : BaseController
    {
        private const string GET_ALL_BOOK_URL = "api/Book/GetAll";
        // GET: Book
        public ActionResult Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync(GET_ALL_BOOK_URL).Result;
            var response = responseMessage.Content.ReadAsAsync<GetAllBookResponse>().Result;
            var model = new BookGetAllViewModel();
            if (response.Success)
            {
                foreach (var book in response.Data)
                {
                    model.Books.Add(new BookViewModel
                    {
                         Id = book.Id,
                         Title = book.Title,
                         Description = book.Description,
                         Price = book.Price,
                         Category = new CategoryViewModel
                         {
                              Id = book.Category.Id,
                              Name = book.Category.Name
                         }
                        
                    });
                }
            }
            return View(model);
        }
    }
}