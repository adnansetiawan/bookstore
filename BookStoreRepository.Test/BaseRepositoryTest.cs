using BookStore.Contracts.DAL;
using BookStore.DAL;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Test
{
    [TestClass]
    public class BaseRepositoryTest
    {
        protected BookStoreEntities context;
        protected IUnitOfWork unitOfWork;

        [TestInitialize]
        public virtual void Setup()
        {
            var connection = DbConnectionFactory.CreateTransient();
            context = new BookStoreEntities(connection);
            context.Database.CreateIfNotExists();
            unitOfWork = new EFUnitOfWork(context);

        }
        [TestCleanup]
        public virtual void Cleanup()
        {
            context = null;
            unitOfWork = null;
        }
    }
}
