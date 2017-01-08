using BookStore.Entities.Databases;
using BookStore.Entities.Inputs.Book;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Mock
{
    public class BookMock
    {
        public static List<Book> GetAll()
        {
            var _fakeBook = new List<Book>
             {
                    new Book { Id = 1, Title = "Lord Of The Ring" },
                    new Book { Id = 2, Title = "Game Of Throne" }
             };
            return _fakeBook;
        }

        public static CreateNewBookInput GetValidInputMock()
        {
            var newBookInput = new CreateNewBookInput()
            {
                CategoryId = 1,
                Price = 5,
                Description = "Book Description",
                Title = "Clean Code"

            };
            return newBookInput;
        }

        public static CreateNewBookInput GetInputWithNotValidCategoryMock()
        {
            var newBookInput = new CreateNewBookInput()
            {
                CategoryId = 0,
               
            };
            return newBookInput;
        }


    }
}
