using BookStore.DAL;
using BookStore.Entities.Databases;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Mock
{
    public class GenericRepositoryMock<T> where T : class
    {
        private DbSet<T> _mockDbSet;
        private BookStoreEntities _bookStoreEntities;
        private List<T> _classMocks;
        private EFGenericRepository<T> _repository;
        public GenericRepositoryMock(List<T> classMocks)
        {
            _classMocks = classMocks;
            var classAsQuerable = classMocks.AsQueryable();
            _mockDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            CreateMockOfDbSet(classAsQuerable);
            CreateMockOfRepository();
            
        }

        private void CreateMockOfDbSet(IQueryable<T> classAsQuerable)
        {
            ((IQueryable<T>)_mockDbSet).Provider.Returns(classAsQuerable.Provider);
            ((IQueryable<T>)_mockDbSet).Expression.Returns(classAsQuerable.Expression);
            ((IQueryable<T>)_mockDbSet).ElementType.Returns(classAsQuerable.ElementType);
            ((IQueryable<T>)_mockDbSet).GetEnumerator().Returns(classAsQuerable.GetEnumerator());
           
        }

        private void CreateMockOfRepository()
        {
            _bookStoreEntities = Substitute.For<BookStoreEntities>();
            _bookStoreEntities.Set<T>().Returns(_mockDbSet);
            _repository = new EFGenericRepository<T>(_bookStoreEntities);

        }

        public EFGenericRepository<T> GetRepositoryMock()
        {
            return _repository;

        }

        
    }
}
