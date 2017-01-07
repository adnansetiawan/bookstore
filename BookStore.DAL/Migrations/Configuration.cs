namespace BookStore.DAL.Migrations
{
    using Entities.Databases;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStore.DAL.BookStoreEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookStore.DAL.BookStoreEntities context)
        {
            context.Categories.AddOrUpdate(
                 c=>c.Id,
                  new Category { Name = "Biography" },
                  new Category { Name = "Programming"}
                  
                );
            context.SaveChanges();
        }
    }
}
