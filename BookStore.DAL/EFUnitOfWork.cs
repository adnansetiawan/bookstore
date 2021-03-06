﻿using BookStore.Contracts.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BookStoreEntities _context;
        private bool _disposed;
        public EFUnitOfWork()
        {
            _context = new BookStoreEntities();
        }
        public EFUnitOfWork(BookStoreEntities context)
        {
            _context = context;
        }
        

        /// <summary>
        /// Gets the generic repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            return new EFGenericRepository<T>(_context);
        }

        
        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e; 
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// the dispose method is called automatically by the injector depending on the lifestyle
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
