using BookStore.Entities.Databases;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Mock
{
    public class CategoryMock
    {
        public static List<Category> GetList()
        {
            var _fakeCategory = new List<Category>
             {
                    new Category { Id = 1, Name = "Programming" },
                    new Category { Id = 2, Name = "History" }
             };
            return _fakeCategory;
        }

        public static Category GetValidSingle()
        {
            return new Category
            {
                Id = 1,
                Name = "Programming"
            };
        }

        public static Category GetNull()
        {
            return null;
        }



    }
}
