using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Profiles
{
    public class RestaurantTypeProfile : Profile
    {
        public RestaurantTypeProfile()
        {
            CreateMap<RestaurantType, ResponseNamedEntityDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.Name, y => y.MapFrom(u => u.Name));
        }
    }
}
