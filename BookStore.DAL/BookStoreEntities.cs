using BookStore.BussinessObjects;
using BookStore.Contracts;
using System;
using System.Collections.Generic;
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

        public DbSet<Book> Books { get; set; }
    }
}
