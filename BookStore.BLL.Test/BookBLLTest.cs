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
        private BookRepositoryMock _bookRepoMock;
        private CategoryRepositoryMock _categoryRepoMock;
        private BookBLL _bookBLL;
        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _bookRepoMock = new BookRepositoryMock(BookMock.GetList());
            _categoryRepoMock = new CategoryRepositoryMock(CategoryMock.GetList());
           
        }

        [TestCleanup]
        public void Cleanup()
        {
            _unitOfWork = null;
            _bookRepo = null;
            _categoryRepo = null;
            _bookBLL = null;
            _categoryRepoMock = null;
            _bookRepoMock = null;
        }
        public void SetUpBookBLL()
        {
            _bookRepo = _bookRepoMock.Repository;
            _categoryRepo = _categoryRepoMock.Repository;
            _unitOfWork.GetGenericRepository<Book>().ReturnsForAnyArgs(_bookRepo);
            _unitOfWork.GetGenericRepository<Category>().ReturnsForAnyArgs(_categoryRepo);
            _bookBLL = new BookBLL(_unitOfWork);
        }
       

        [TestMethod]
        public void When_GetAll_ReturnValidData()
        {
            SetUpBookBLL();
            var expectedResult = BookMock.GetList();
            var actualResult = _bookBLL.GetAll();
            Assert.AreNotEqual(0, actualResult.Books.Count);
            Assert.AreEqual(expectedResult.Count, actualResult.Books.Count);
        }

        [TestMethod]
        public void When_GetByTitle_ReturnValidData()
        {
            SetUpBookBLL();
            var titleToFind = "Lord";
            var actualResult = _bookBLL.GetByTitle(titleToFind);
            Assert.AreNotEqual(0, actualResult.Books.Count);
            Assert.IsTrue(actualResult.Books.First().Title.Contains(titleToFind));
        }

        [TestMethod]
        public void When_GetDetail_ReturnValidData()
        {
            var expectedId = 1;
            _bookRepoMock.GetByIdMock(expectedId);
            SetUpBookBLL();
            var actualResult = _bookBLL.GetDetail(expectedId);
            Assert.AreEqual(expectedId, actualResult.Book.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BLLException))]
        public void When_Insert_ReturnCategoryNotFound()
        {
            var categoryIdNullData = 0;
            _categoryRepoMock.GetByIdMock(categoryIdNullData);
            SetUpBookBLL();
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

        [TestMethod]
        public void When_Insert_IsSucess()
        {
            var categoryIdValid = 1;
            _categoryRepoMock.GetByIdMock(categoryIdValid);
            SetUpBookBLL();
            _bookBLL.AddNewBook(BookMock.GetValidInputMock());          
        }
    }
}
