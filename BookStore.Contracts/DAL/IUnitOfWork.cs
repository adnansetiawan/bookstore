using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.DAL
{
    public interface IUnitOfWork 
    {
        IGenericRepository<T> GetGenericRepository<T>()
          where T : class;
        
        void SaveChanges();
        void Dispose();
    }
}
