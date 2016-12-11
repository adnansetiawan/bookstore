using DtoInput = BookStore.BussinessObjects.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BLL
{
    public interface ICategoryBLL
    {
        List<DtoInput.CategoryDto> GetAll();
    }
}
