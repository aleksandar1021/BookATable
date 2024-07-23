using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Profiles
{
    internal class MealCategoryProfile : Profile
    {
        public MealCategoryProfile()
        {
            CreateMap<MealCategory, ResponseNamedEntityDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.Name, y => y.MapFrom(u => u.Name));
        }
    }
}
