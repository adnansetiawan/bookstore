using AutoMapper;
using BookStore.BussinessObjects.DAO;
using BookStore.BussinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoOutput = BookStore.BussinessObjects.DTO.Output;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
using BookStore.Core.Utils;
namespace BookStore.BLL.Mapping
{
    public class DaoToDtoMapper 
    {
      
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(
            cfg => {
            cfg.CreateMap<Category, DtoOutput.CategoryDto>()
                 .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name)).IgnoreAllUnmapped();
            cfg.CreateMap<Book, DtoOutput.BookDto>();
            cfg.CreateMap<DtoInput.BookDto, Book>()
                .ForMember(dest => dest.Category, opts => opts.Ignore());



            });
            
            
        }
    }
}
