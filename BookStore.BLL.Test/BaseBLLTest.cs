using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Test
{
    public class BaseBLLTest
    {
        public BaseBLLTest()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile(new BLL.Mapping.DbEntityToDtoMapper());
                cfg.AddProfile(new BLL.Mapping.DtoToDbEntityMapper());
               
            });
        }
    }
}
