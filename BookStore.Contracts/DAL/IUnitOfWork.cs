using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>()
          where T : class;
        
        void SaveChanges();
    }
}
