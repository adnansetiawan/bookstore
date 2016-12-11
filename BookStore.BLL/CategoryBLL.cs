using BookStore.Contracts.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BussinessObjects.DAO;
using BookStore.Contracts.DAL;
using DtoOutput = BookStore.BussinessObjects.DTO.Output;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
using AutoMapper;
using BookStore.BLL.Validator;

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

        
        public List<DtoOutput.CategoryDto> GetAll()
        {
            var categories = _categoryRepo.Get();
            var categoriesDto = Mapper.Map<List<DtoOutput.CategoryDto>>(categories);
            return categoriesDto;
        }

        public void AddNewCategory(DtoInput.CategoryDto NewCategory)
        {
            var newCategory = Mapper.Map<Category>(NewCategory);
            _categoryRepo.Insert(newCategory);
            _unitOfWork.SaveChanges();
        }
    }
}
