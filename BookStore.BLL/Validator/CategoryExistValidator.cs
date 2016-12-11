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
    public class CategoryExistValidator : IValidator<DtoInput.CategoryDto>
    {
        IGenericRepository<BussinessObjects.DAO.Category> _categoryRepository;
        public CategoryExistValidator(IGenericRepository<BussinessObjects.DAO.Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Validate(DtoInput.CategoryDto model)
        {
            var category = _categoryRepository.GetById(model.Id);
            if (category == null)
                throw new BLLException(ExceptionCodes.BLLExceptions.CategoryNotFound);
            
        }
    }
}
