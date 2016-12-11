using BookStore.BussinessObjects.DAO;
using BookStore.Contracts.DAL;
using BookStore.Contracts.Services;
using BookStore.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
namespace BookStore.BLL.Validator
{
    public class BookValidator : IValidator<DtoInput.BookDto>
    {
        private CategoryExistValidator _categoryExistValidator;
        private readonly IGenericRepository<Category> _categoryRepo;
        public BookValidator(IGenericRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _categoryExistValidator = new CategoryExistValidator(categoryRepo);

        }
        public void Validate(DtoInput.BookDto model)
        {
            if (string.IsNullOrEmpty(model.Title))
                throw new BLLException(ExceptionCodes.BLLExceptions.TitleIsNullOrEmpty);
            if (_categoryExistValidator != null)
                _categoryExistValidator.Validate(new BussinessObjects.DTO.Input.CategoryDto() { Id = model.CategoryId });
        }
    }
}
