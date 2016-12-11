using AutoMapper;
using BookStore.BussinessObjects.DAO;
using BookStore.BussinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoInput = BookStore.BussinessObjects.DTO.Input;
using BookStore.Core.Utils;
namespace BookStore.BLL.Mapping
{
    public class DtoToDaoMapper : Profile
    {

        public DtoToDaoMapper()
        {
            CreateMap<DtoInput.CategoryDto, Category>()
                     .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                     .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        }
        
    }
}
