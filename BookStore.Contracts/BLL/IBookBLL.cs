using BookStore.BussinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoOutput = BookStore.BussinessObjects.DTO.Output;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
namespace BookStore.Contracts.BLL
{
    public interface IBookBLL
    {
        List<DtoOutput.BookDto> GetAll();
        void AddNewBook(DtoInput.BookDto book);
    }
}
