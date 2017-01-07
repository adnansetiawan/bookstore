using BookStore.Contracts.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Databases;
using BookStore.Contracts.DAL;
using AutoMapper;
using BookStore.Entities.DTOs;
using BookStore.Entities.Outputs.Book;
using BookStore.Entities.Inputs.Book;
using BookStore.Common.Exceptions;

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

        
        public GetAllBookOutput GetAll()
        {
            var bookOutput = new GetAllBookOutput();
            var booksDto = new List<BookDto>();
            try
            {
                var books = _bookRepo.Get();
                booksDto = Mapper.Map<List<BookDto>>(books);
            }
            catch(BLLException ex)
            {
                bookOutput.Success = false;
                bookOutput.Messages = ex.Message;
            }
            return new GetAllBookOutput
            {
                Books = booksDto
            };
        }

        public void AddNewBook(CreateNewBookInput newBookInput)
        {

            var category = _categoryRepo.GetById(newBookInput.CategoryId);
            if (category == null)
                throw new BLLException(ExceptionCodes.BLLExceptions.CategoryNotFound);
            try
            {
                var newBook = Mapper.Map<Book>(newBookInput);
                newBook.Category = category;
                _bookRepo.Insert(newBook);
                
            }
            catch (BLLException ex)
            {
                throw new BLLException(ExceptionCodes.BLLExceptions.UnhandledError, ex.Message);
            }
           
        }
    }
}
