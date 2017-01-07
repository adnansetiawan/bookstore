using BookStore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Outputs.Book
{
    public class GetBookDetailOutput : BaseOutput
    {
        public BookDto Book { get; set; }
    }
}
