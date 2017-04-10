using BookStore.WebApi.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebApi.Models.Response.Book
{
    public class BookResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public CategoryResponse Category {get; set;}
    }
}