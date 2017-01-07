using AutoMapper;
using BookStore.Entities.Inputs.Category;
using BookStore.WebApi.Models.Request.Category.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace BookStore.WebApi.Mapper
{
    public class ModelToDtoMapper : Profile
    {
        public ModelToDtoMapper()
        {
            CreateMap<CreateCategoryRequest, CreateNewCategoryInput>();
        }
    }
}