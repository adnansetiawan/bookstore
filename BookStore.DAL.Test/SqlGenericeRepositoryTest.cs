using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using BookStore.BussinessObjects.Dao;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BookStore.Contracts;

namespace BookStore.DAL.Test
{
    [TestClass]
    public class SqlGenericeRepositoryTest
    {
        private BookStoreEntities _bookEntities;
        private DbSet<Book> _mockSet;
        private List<Book> _fakeBook;
        private IGenericRepository<Book> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _bookEntities = Substitute.For<BookStoreEntities>();
            _fakeBook = new List<Book>
             {
                    new Book { Id = 1, Title = "Lord Of The Ring" },
                    new Book { Id = 2, Title = "Game Of Throne" }
             };
            var data = QuerableBook;
            _mockSet = Substitute.For<DbSet<Book>, IQueryable<Book>>();
            ((IQueryable<Book>)_mockSet).Provider.Returns(data.Provider);
            ((IQueryable<Book>)_mockSet).Expression.Returns(data.Expression);
            ((IQueryable<Book>)_mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<Book>)_mockSet).GetEnumerator().Returns(data.GetEnumerator());
            _bookEntities.Set<Book>().Returns(_mockSet);
            _repository = new SqlGenericRepository<Book>(_bookEntities);
            
        }

        private List<Book> FakeBook
        {
            set { value = _fakeBook; }
            get
            {
                return _fakeBook;
            }
            
           

            
        }
        private IQueryable<Book> QuerableBook
        {
            get
            {
                return FakeBook.AsQueryable();
            }


        }

        [TestCleanup]
        public void CleanUp()
        {
            _bookEntities = null;
            _fakeBook = null;
            _mockSet = null;
            _repository = null;

        }
        [TestMethod]
        public void BookRepositoryGetTest()
        {
            var result = _repository.Get();
            Assert.AreEqual(FakeBook.Count, result.Count());
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
