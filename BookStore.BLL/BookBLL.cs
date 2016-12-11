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
    public class BookBLL : IBookBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        public BookBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepo = _unitOfWork.GetGenericRepository<Book>();
            _categoryRepo = _unitOfWork.GetGenericRepository<Category>();
        }

        
        public List<DtoOutput.BookDto> GetAll()
        {
            var books = _bookRepo.Get();
            var booksDto = Mapper.Map<List<DtoOutput.BookDto>>(books);
            return booksDto;
        }

        public void AddNewBook(DtoInput.BookDto book)
        {

            var bookValidator = new BookValidator(_categoryRepo);
            bookValidator.Validate(book);
            var category = _categoryRepo.GetById(book.CategoryId);
            var newBook = Mapper.Map<Book>(book);
            _bookRepo.Insert(newBook);
           
        }
    }
}
