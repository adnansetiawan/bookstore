using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Contracts.DAL;
using BookStore.Entities.DTOs;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using BookStore.Common.Exceptions;
using BookStore.BLL;
using BookStore.Entities.Databases;
using BookStore.Repository.Mock;
using BookStore.Entities.Mock;
using BookStore.Entities.Inputs.Book;


namespace BookStore.BLL.Test
{
    [TestClass]
    public class BookBLLTest : BaseBLLTest
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Book> _bookRepo;
        private IGenericRepository<Category> _categoryRepo;
       
        private BookBLL _bookBLL;
        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _bookRepo = new GenericRepositoryMock<Book>(BookMock.GetAll()).GetRepositoryMock();
            _categoryRepo = new GenericRepositoryMock<Category>(CategoryMock.GetAll()).GetRepositoryMock();
            _unitOfWork.GetGenericRepository<Book>().ReturnsForAnyArgs(_bookRepo);
            _unitOfWork.GetGenericRepository<Category>().ReturnsForAnyArgs(_categoryRepo);
            _bookBLL = new BookBLL(_unitOfWork);
            
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
            var expectedResult = BookMock.GetAll();
            var actualResult = _bookBLL.GetAll();
            Assert.AreNotEqual(0, actualResult.Books.Count);
            Assert.AreEqual(expectedResult.Count, actualResult.Books.Count);
        }

       
        [TestMethod]
        [ExpectedException(typeof(BLLException))]
        public void When_Insert_CategoryIsNotFound()
        {
            _categoryRepo.GetById(0).ReturnsForAnyArgs(CategoryMock.GetNull());

            var newBook = new CreateNewBookInput()
            {
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
