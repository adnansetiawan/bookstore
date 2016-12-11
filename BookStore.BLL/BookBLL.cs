using BookStore.Contracts.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BussinessObjects.Dao;
using BookStore.Contracts;
using BookStore.BussinessObjects.Dto;
using AutoMapper;
namespace BookStore.BLL
{
    public class BookBLL : IBookBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Book> _bookRepo;

        public BookBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepo = _unitOfWork.GetGenericRepository<Book>();           
        }

        
        public List<BookDto> GetAll()
        {
            var books = _bookRepo.Get();
            var booksDto = Mapper.Map<List<BookDto>>(books);
            return booksDto;
        }
    }
}
