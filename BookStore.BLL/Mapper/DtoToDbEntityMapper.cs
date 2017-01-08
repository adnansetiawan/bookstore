using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Common.Utils;
using BookStore.Entities.Databases;
using BookStore.Entities.DTOs;

namespace BookStore.BLL.Mapping
{
    public class DtoToDbEntityMapper : Profile
    {

        public DtoToDbEntityMapper()
        {
            CreateMap<CategoryDto, Category>()
                     .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                     .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        }
        
    }
}
