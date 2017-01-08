using AutoMapper;
using BookStore.Entities.Databases;
using BookStore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Common.Utils;
namespace BookStore.BLL.Mapping
{
    public class DbEntityToDtoMapper : Profile

    {
        public DbEntityToDtoMapper()
        {
            CreateMap<Category, CategoryDto>()
                     .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                     .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));
            CreateMap<Book, BookDto>();
        }

        
    }
}
