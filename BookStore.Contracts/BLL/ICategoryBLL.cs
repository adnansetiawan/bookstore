using DtoOutput = BookStore.BussinessObjects.DTO.Output;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BLL
{
    public interface ICategoryBLL
    {
        List<DtoOutput.CategoryDto> GetAll();
        void AddNewCategory(DtoInput.CategoryDto NewCategory);
    }
}
