using BookStore.Entities.Databases;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Mock
{
    public class CategoryRepositoryMock : GenericRepositoryMock<Category>
    {
        public CategoryRepositoryMock(List<Category> mockCategory) : base(mockCategory)
        {
            
        }
        public void GetByIdMock(int Id)
        {
            this.mockDbSet.Find(Id).Returns(
                callInfo =>
                {
                    var m = callInfo.Args().ToList();
                    Int32? value = 0;
                    foreach (var n in m)
                    {
                        var k = n as object[];
                        var i = k[0];
                        value = i as Nullable<Int32>;
                    }
                    return this.classMocks.FirstOrDefault(x => x.Id == value);
                });
        }
    }
}
