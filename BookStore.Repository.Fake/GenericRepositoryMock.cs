using BookStore.DAL;
using BookStore.Entities.Databases;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Mock
{
    public class GenericRepositoryMock<T> where T : class
    {
        protected DbSet<T> mockDbSet;
        private BookStoreEntities _bookStoreEntities;
        protected List<T> classMocks;
        public EFGenericRepository<T> Repository;
        public GenericRepositoryMock(List<T> classMocks)
        {
            this.classMocks = classMocks;
            var classAsQuerable = classMocks.AsQueryable();
            mockDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            CreateMockOfDbSet(classAsQuerable);
            CreateMockOfRepository();

        }

        private void CreateMockOfDbSet(IQueryable<T> classAsQuerable)
        {
            ((IQueryable<T>)mockDbSet).Provider.Returns(classAsQuerable.Provider);
            ((IQueryable<T>)mockDbSet).Expression.Returns(classAsQuerable.Expression);
            ((IQueryable<T>)mockDbSet).ElementType.Returns(classAsQuerable.ElementType);
            ((IQueryable<T>)mockDbSet).GetEnumerator().Returns(classAsQuerable.GetEnumerator());
           
        }

        private void CreateMockOfRepository()
        {
            _bookStoreEntities = Substitute.For<BookStoreEntities>();
            _bookStoreEntities.Set<T>().Returns(mockDbSet);
            Repository = new EFGenericRepository<T>(_bookStoreEntities);
           
        }
        
       

        
    }
}
