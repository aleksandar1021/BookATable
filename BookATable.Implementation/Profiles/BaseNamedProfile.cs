using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Profiles
{
    public class BaseNamedProfile<TSource, TDestination> : Profile where TDestination : ResponseNamedEntityDTO where TSource : NamedEntity
    {
        public BaseNamedProfile()
        {
            CreateMap<TSource, TDestination>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
