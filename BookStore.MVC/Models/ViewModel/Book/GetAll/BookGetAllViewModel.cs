using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models.ViewModel.Book.GetAll
{
    public class BookGetAllViewModel
    {
        public BookGetAllViewModel()
        {
            Books = new List<BookViewModel>();
        }
        public List<BookViewModel> Books { get; set; }
    }
}