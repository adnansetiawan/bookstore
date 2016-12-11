using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Contracts.DAL;
using BookStore.BussinessObjects.DAO;
using DtoOutput = BookStore.BussinessObjects.DTO.Output;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using BookStore.Core.Exceptions;

namespace BookStore.BLL.Test
{
    [TestClass]
    public class BookBLLTest
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Book> _bookRepo;
        private IGenericRepository<Category> _categoryRepo;

        private BookBLL _bookBLL;
        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _bookRepo = Substitute.For<IGenericRepository<Book>>();
            _categoryRepo = Substitute.For<IGenericRepository<Category>>();
            _unitOfWork.GetGenericRepository<Book>().ReturnsForAnyArgs(_bookRepo);
            _bookBLL = new BookBLL(_unitOfWork);
            Mapping.DaoToDtoMapper.Initialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _unitOfWork = null;
            _bookRepo = null;
            _categoryRepo = null;
            _bookBLL = null;

        }

        [TestMethod]
        public void When_GetAll_ReturnValidData()
        {
            var books = new List<Book>
            {
                new Book {  Id = 1, Title = "Lord Of The Ring" },
                new Book {  Id = 2, Title = "Game of Throne"}
            };
            _bookRepo.Get().ReturnsForAnyArgs(books);
            var result = _bookBLL.GetAll();
            Assert.AreEqual(books.Count, result.Count);
            Assert.AreEqual(books.First().Id, result.First().Id);
                
        }

        [TestMethod]
        [ExpectedException(typeof(BLLException))]
        public void When_Insert_TitleIsNullOrEmpty()
        {
            Category category = null;
            _categoryRepo.GetById(0).ReturnsForAnyArgs(category);

            var newBook = new DtoInput.BookDto()
            {
               
            };
            try
            {
                _bookBLL.AddNewBook(newBook);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BLLException));
                Assert.AreEqual(ExceptionCodes.BLLExceptions.TitleIsNullOrEmpty.ToString(), ((BLLException)ex).Code);
                throw ex;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(BLLException))]
        public void When_Insert_CategoryNotValid()
        {
            Category category = null;
            _categoryRepo.GetById(0).ReturnsForAnyArgs(category);

            var newBook = new DtoInput.BookDto()
            {
                Title = "New Title",
                CategoryId = 0
            };
            try
            {
                _bookBLL.AddNewBook(newBook);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BLLException));
                Assert.AreEqual(ExceptionCodes.BLLExceptions.CategoryNotFound.ToString(), ((BLLException)ex).Code);
                throw ex;
            }


        }
    }
}
