using BookStore.MVC.Models.ApiResponse.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Services.Contract
{
    public interface IBookService
    {
        GetAllBookResponse GetAllBook();
        CreateNewBookResponse CreateNewBook(BookRequest newBook);
    }
}