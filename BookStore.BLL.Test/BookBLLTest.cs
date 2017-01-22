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
        public override void Setup()
        {
            base.Setup();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _bookRepo = Substitute.For<IGenericRepository<Book>>();
            _unitOfWork.GetGenericRepository<Book>().ReturnsForAnyArgs(_bookRepo);
            _categoryRepo = Substitute.For<IGenericRepository<Category>>();
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
        [TestCategory("BookBLL")]
        public void When_GetAll_Then_All()
        {
           //arrange
            var expectedResult = BookMock.GetList();
            _bookRepo.Get().Returns(expectedResult);

            //act
            var actualResult = _bookBLL.GetAll();
          
            //assert
            Assert.AreEqual(expectedResult.Count, actualResult.Books.Count);
            Assert.AreEqual(expectedResult.First().Title, actualResult.Books.First().Title);
        }

       

        [TestMethod]
        public void When_GetDetail_ReturnValidData()
        {
            var firstBook = BookMock.GetList().First();
            _bookRepo.GetById(firstBook.Id).Returns(firstBook);
            var actualResult = _bookBLL.GetDetail(firstBook.Id);
            Assert.AreEqual(firstBook.Id, actualResult.Book.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BLLException))]
        public void When_Insert_ReturnCategoryNotFound()
        {
            var categoryNull = CategoryMock.GetNull();
            _categoryRepo.GetById(0).Returns(categoryNull);
            try
            {
                _bookBLL.AddNewBook(BookMock.GetInputWithNotValidCategoryMock());
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
