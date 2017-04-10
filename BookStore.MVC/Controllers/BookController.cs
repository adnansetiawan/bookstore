using BookStore.MVC.Models.ApiResponse.Book;
using BookStore.MVC.Models.ViewModel.Book;
using BookStore.MVC.Models.ViewModel.Category;
using BookStore.MVC.Services.Contract;
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
        private IBookService _bookService;
        private ICategoryService _categoryService;
        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }
        // GET: Book
        public ActionResult Index()
        {
            var model = new BookGetAllViewModel();
            var getAllBookResponse = _bookService.GetAllBook();
            if (getAllBookResponse.Success)
            {
                foreach (var book in getAllBookResponse.Data)
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
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var model = new FormBookViewModel();
            var categoryResponse = _categoryService.GetAllCategory();
            if (categoryResponse.Success)
            {
                model.CategorySelectList = new SelectList(categoryResponse.Data, "Id", "Name");
            }

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(FormBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var newBookRequest = new BookRequest
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId
            };
            _bookService.CreateNewBook(newBookRequest);
            return RedirectToAction("Index");
        }

        
    }
}