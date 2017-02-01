using BookStore.MVC.Models.ApiResponse.Book;
using BookStore.MVC.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BookStore.MVC.Services
{
    public class BookService : BaseService,IBookService
    {
        protected const string GET_ALL_BOOK_URL = "api/Book/GetAll";
        protected const string ADD_NEW_BOOK_URL = "api/Book/Create";

        public CreateNewBookResponse CreateNewBook(BookRequest newBook)
        {
            
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync(ADD_NEW_BOOK_URL, newBook).Result;
            var response = responseMessage.Content.ReadAsAsync<CreateNewBookResponse>().Result;
            return response;

        }

        public GetAllBookResponse GetAllBook()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync(GET_ALL_BOOK_URL).Result;
            var response = responseMessage.Content.ReadAsAsync<GetAllBookResponse>().Result;
            return response;
        }
    }
}