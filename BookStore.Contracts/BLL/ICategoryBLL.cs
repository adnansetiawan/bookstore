using BookStore.Entities.Outputs;
using BookStore.Entities.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Outputs.Category;
using BookStore.Entities.Inputs.Category;

namespace BookStore.Contracts.BLL
{
    public interface ICategoryBLL
    {
        GetAllCategoryOutput GetAll();
        void AddNewCategory(CreateNewCategoryInput NewCategory);
    }
}
