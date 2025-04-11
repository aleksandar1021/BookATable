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
    public class RegularClosedDaysProfile : Profile
    {
        public RegularClosedDaysProfile()
        {
            CreateMap<Domain.Tables.RegularClosedDays, ResponseRegularClosedDaysDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.Restaurant, y => y.MapFrom(u => u.RestaurantId))
               .ForMember(x => x.Day, y => y.MapFrom(u => u.DayOfWeek))
               .ForMember(x => x.Restaurant, y => y.MapFrom(u => new BaseResponseRestaurantDTO
               {
                   Id = u.Id,
                   Name = u.Restaurant.Name,
                   Description = u.Restaurant.Description,
                   UserId = u.Restaurant.UserId
               }));
        }
    }
}
