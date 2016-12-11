using AutoMapper;
using BookStore.WebApi.Models.Request.Category.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
namespace BookStore.WebApi.Mapping
{
    public class ModelToDtoMapper : Profile
    {
        public ModelToDtoMapper()
        {
            CreateMap<CreateCategoryRequest, DtoInput.CategoryDto>();
        }
    }
}