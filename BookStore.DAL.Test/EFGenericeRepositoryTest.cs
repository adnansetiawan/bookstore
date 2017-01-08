using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BookStore.Contracts.DAL;
using BookStore.DAL;
using BookStore.Entities.Databases;
using BookStore.Entities.Fake;

namespace BookStore.Tests.DAL.Test
{
    [TestClass]
    public class SqlGenericeRepositoryTest
    {
        private BookStoreEntities _bookEntities;
        private IGenericRepository<Book> _repository;
        [TestInitialize]
        public void SetUp()
        {
            _bookEntities = Substitute.For<BookStoreEntities>();
            _bookEntities.Set<Book>().Returns(BookFake.GetBookDbSet());
            _repository = new EFGenericRepository<Book>(_bookEntities);
            
        }

       
        [TestCleanup]
        public void CleanUp()
        {
            _bookEntities = null;
            _repository = null;

        }
        [TestMethod]
        public void BookRepositoryGetTest()
        {
            var result = _repository.Get();
            var expectedResult = BookFake.GetAll().Count;
            var actualResult = result.Count();
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void BookRepositoryGeWithtExpressionTest()
        {
            var result = _repository.Get(x=>x.Id == 2);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result.First().Id);
        }

        [TestMethod]
        public void BookRepositoryGetByIdTest()
        {
            _mockSet.Find(Arg.Any<int>()).Returns(
                callInfo =>
                {
                    var m = callInfo.Args().ToList();
                    Int32? value = 0;
                    foreach (var n in m)
                    {
                        var k = n as object[];
                        var i = k[0];
                        value = i as Nullable<Int32>;
                    }
                    
                    
                    return FakeBook.FirstOrDefault(x => x.Id ==value);
                });

            var result = _repository.GetById(1);
            Assert.AreEqual(1, (result as Book).Id);
        }

        [TestMethod]
        public void BookRepositoryInsertTest()
        {
            var bookToAdd = new Book()
            {
                Id = 3,
                Title = "The Hobbit"
            };
            _mockSet.Add(bookToAdd).Returns(callInfo =>
               {
                   FakeBook.Add(bookToAdd);
                   return bookToAdd;
               }
            );

            _repository.Insert(bookToAdd);
            var result = FakeBook.Count;
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void BookRepositoryDeleteTest()
        {
            var bookToRemove = FakeBook.First();
            _mockSet.Remove(bookToRemove).Returns(callInfo =>
            {
                FakeBook.Remove(bookToRemove);
                return bookToRemove;
            }
            );

            _repository.Delete(bookToRemove);
            var result = FakeBook.Count;
            Assert.AreEqual(1, result);
        }

    }
}
