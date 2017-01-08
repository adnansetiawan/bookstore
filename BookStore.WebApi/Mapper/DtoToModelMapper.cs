using AutoMapper;
using BookStore.Entities.DTOs;
using BookStore.Entities.Outputs;
using BookStore.Entities.Outputs.Category;
using BookStore.WebApi.Models.Response;
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
                .Include<GetAllCategoryOutput, GetAllCategoryResponse>();

            CreateMap<CategoryDto, CategoryResponse>();
            CreateMap<GetAllCategoryOutput, GetAllCategoryResponse>()
                .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Categories));
             
        }
    }
}