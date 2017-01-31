using AutoMapper;
using BookStore.Entities.DTOs;
using BookStore.Entities.Outputs;
using BookStore.Entities.Outputs.Book;
using BookStore.Entities.Outputs.Category;
using BookStore.WebApi.Models.Response;
using BookStore.WebApi.Models.Response.Book;
using BookStore.WebApi.Models.Response.Book.GetAll;
using BookStore.WebApi.Models.Response.Category;
using BookStore.WebApi.Models.Response.Category.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace BookStore.WebApi.Mapper
{
    public class DtoToModelMapper : Profile
    {
        public DtoToModelMapper()
        {
            CreateMap<BaseOutput, BaseMessageResponse>()
                .Include<GetAllBookOutput, GetAllBookResponse>();
        
            CreateMap<CategoryDto, CategoryResponse>();
            CreateMap<BookDto, BookResponse>();
            CreateMap<GetAllBookOutput, GetAllBookResponse>()
               .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Books));


            CreateMap<GetAllCategoryOutput, GetAllCategoryResponse>()
                .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Categories));
             
        }
    }
}