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
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, ResponseDishDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.Name, y => y.MapFrom(u => u.Name))
               .ForMember(x => x.Description, y => y.MapFrom(u => u.Description))
               .ForMember(x => x.Price, y => y.MapFrom(u => u.Price))
               .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId));
        }
    }
}
