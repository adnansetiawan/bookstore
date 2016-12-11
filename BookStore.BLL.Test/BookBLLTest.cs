using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Contracts;
using BookStore.BussinessObjects.Dao;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BLL.Test
{
    [TestClass]
    public class BookBLLTest
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Book> _bookRepo;
        private BookBLL _bookBLL;
        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _bookRepo = Substitute.For<IGenericRepository<Book>>();
            _unitOfWork.GetGenericRepository<Book>().ReturnsForAnyArgs(_bookRepo);
            _bookBLL = new BookBLL(_unitOfWork);
            Mapping.EntityToDtoMapper.Initialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _unitOfWork = null;
            _bookRepo = null;
            _bookBLL = null;
        }

        [TestMethod]
        public void GetAllTest()
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
    }
}
