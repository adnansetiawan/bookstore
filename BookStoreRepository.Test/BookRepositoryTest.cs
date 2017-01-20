using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.DAL;
using Effort;
using BookStore.Contracts.DAL;
using BookStore.Entities.Databases;
using Effort.Provider;
using System.Linq;
using Effort.DataLoaders;
using BookStore.Entities.Mock;

namespace BookStoreRepository.Test
{
    [TestClass]
    public class BookRepositoryTest : BaseRepositoryTest
    {
        private IGenericRepository<Book> _bookRepository;
        [TestInitialize]
        public override void Setup()
        {

            base.Setup();
            context.Categories.Add(CategoryMock.GetValidSingle());
            context.SaveChanges();
            var category = context.Categories.First();
            context.Books.AddRange(BookMock.GetList());
            context.SaveChanges();
            _bookRepository = unitOfWork.GetGenericRepository<Book>();

        }
        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            _bookRepository = null;
        }
        [TestMethod]
        public void GetAll()
        {
            var books = _bookRepository.Get().ToList();
            Assert.AreNotEqual(0, books.Count);
        }
        [TestMethod]
        public void FindByName()
        {
            var firstBook = BookMock.GetList().First();
            var books = _bookRepository.Get(x => x.Title.Contains(firstBook.Title)).ToList();
            Assert.AreNotEqual(0, books.Count);
            Assert.AreEqual(firstBook.Id, books.First().Id);
        }
        [TestMethod]
        public void GetById()
        {
            var firstBook = BookMock.GetList().First();
            var book = _bookRepository.GetById(firstBook.Id);
            Assert.IsNotNull(book);
            Assert.AreEqual(firstBook.Id, book.Id);
        }

        
    }
}
