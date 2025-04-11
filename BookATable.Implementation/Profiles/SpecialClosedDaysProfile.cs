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
    public class SpecialClosedDaysProfile : Profile
    {
        public SpecialClosedDaysProfile()
        {
            CreateMap<Domain.Tables.SpecificClosedDays, ResponseSpecificClosedDays>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId))
               .ForMember(x => x.ClosedFrom, y => y.MapFrom(u => u.ClosedFrom))
               .ForMember(x => x.ClosedTo, y => y.MapFrom(u => u.ClosedTo))
               .ForMember(x => x.Restaurant, y => y.MapFrom(u => new BaseResponseRestaurantDTO
               {
                   Id = u.Id,
                   Name = u.Restaurant.Name,
                   Description = u.Restaurant.Description,
                   UserId = u.Restaurant.Id
               }));
        }
    }
}
