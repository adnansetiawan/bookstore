using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BookStore.BLL.Test
{
    [TestClass]
    public class BaseBLLTest
    {
        [TestInitialize]
        public virtual void Setup()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new BLL.Mapping.DbEntityToDtoMapper());
                cfg.AddProfile(new BLL.Mapping.DtoToDbEntityMapper());

            });
        }
    }
}
