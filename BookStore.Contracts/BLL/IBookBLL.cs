using BookStore.Entities.Outputs.Book;
using BookStore.Entities.Inputs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BLL
{
    public interface IBookBLL
    {
        GetAllBookOutput GetAll();
        GetBookDetailOutput GetDetail(int Id);
        GetAllBookOutput GetByTitle(string Title);
        void AddNewBook(CreateNewBookInput newBookInput);
    }
}
