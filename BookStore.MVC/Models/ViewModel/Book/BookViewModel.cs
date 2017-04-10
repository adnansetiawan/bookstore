using BookStore.MVC.Models.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models.ViewModel.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}