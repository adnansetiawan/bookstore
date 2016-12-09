using BookStore.BussinessObjects.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BLL
{
    public interface IBookBLL
    {
        List<BookDto> GetAll();
    }
}
