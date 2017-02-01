using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.MVC.Models.ViewModel.Book
{
    public class FormBookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public SelectList CategorySelectList { get; set; }
    }
}