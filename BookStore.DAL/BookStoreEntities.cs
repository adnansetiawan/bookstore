using BookStore.Contracts;
using BookStore.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL
{
   
    public class BookStoreEntities : DbContext 
    {
        public BookStoreEntities() : base("name=BookStoreConnection")
        {
        }
        public BookStoreEntities(DbConnection connection) : base(connection, true)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
