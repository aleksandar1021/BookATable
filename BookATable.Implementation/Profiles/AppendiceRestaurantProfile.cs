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
    public class AppendiceRestaurantProfile : Profile
    {
        public AppendiceRestaurantProfile()
        {
            CreateMap<AppendiceRestaurant, ResponseAppendiceRestaurantDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId))
               .ForMember(x => x.Appendice, y => y.MapFrom(u => u.AppendiceId))
               .ForMember(x => x.Restaurant, y => y.MapFrom(u => new ResponseRestaurantForMealCategoryRestaurantDTO
               {
                   Id = u.Id,
                   Name = u.Restaurant.Name,
                   Description = u.Restaurant.Description,
                   UserId = u.Restaurant.UserId
               }))
               .ForMember(x => x.Appendice, y => y.MapFrom(u => new ResponseNamedEntityDTO
               {
                   Id = u.Id,
                   Name = u.Restaurant.Name
               }));
        }
    }
}
