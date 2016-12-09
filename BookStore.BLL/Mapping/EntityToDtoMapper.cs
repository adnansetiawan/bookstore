using AutoMapper;
using BookStore.BussinessObjects;
using BookStore.BussinessObjects.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Mapping
{
    public class EntityToDtoMapper 
    {
      
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Book, BookDto>();
            });
            
        }
    }
}
