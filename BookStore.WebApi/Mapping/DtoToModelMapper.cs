using AutoMapper;
using BookStore.WebApi.Models.Request.Category.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DtoOutput = BookStore.BussinessObjects.DTO.Output;
namespace BookStore.WebApi.Mapping
{
    public class DtoToModelMapper : Profile
    {
        public DtoToModelMapper()
        {
            CreateMap<CreateCategoryRequest, DtoOutput.CategoryDto>();
        }
    }
}