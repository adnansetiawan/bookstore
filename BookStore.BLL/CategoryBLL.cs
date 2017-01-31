using BookStore.Contracts.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Databases;
using BookStore.Entities.DTOs;
using BookStore.Contracts.DAL;
using AutoMapper;
using BookStore.Entities.Outputs.Category;
using BookStore.Entities.Inputs.Category;
using BookStore.Common.Exceptions;

namespace BookStore.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _categoryRepo;
        public CategoryBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = _unitOfWork.GetGenericRepository<Category>();
        }

        
        public GetAllCategoryOutput GetAll()
        {
            var categoryOutput = new GetAllCategoryOutput();
            var categoriesDto = new List<CategoryDto>();
            try
            {
                var categories = _categoryRepo.Get();
                categoriesDto = Mapper.Map<List<CategoryDto>>(categories);

            }
            catch (BLLException ex)
            {
                categoryOutput.Success = false;
                categoryOutput.Messages = ex.Message;
            }
            return new GetAllCategoryOutput
            {
                Categories = categoriesDto
            };

        }

        public void AddNewCategory(CreateNewCategoryInput newCategoryInput)
        {
            var newCategory = Mapper.Map<Category>(newCategoryInput);
            try
            {
                _categoryRepo.Insert(newCategory);
                _unitOfWork.SaveChanges();
                
            }
            catch (BLLException ex)
            {
                throw new BLLException(ExceptionCodes.BLLExceptions.UnhandledError, ex.Message);
            }
           
        }
    }
}
