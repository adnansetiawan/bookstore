using BookStore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Outputs.Book
{
    public class GetAllBookOutput : BaseOutput
    {
        public GetAllBookOutput()
        {
            Books = new List<BookDto>();
        }
        public List<BookDto> Books { get; set; }
    }
}
