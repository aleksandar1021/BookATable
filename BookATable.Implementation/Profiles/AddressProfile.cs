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
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, ResponseAddressDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.CityId, y => y.MapFrom(u => u.CityId))
               .ForMember(x => x.Place, y => y.MapFrom(u => u.Place))
               .ForMember(x => x.Address, y => y.MapFrom(u => u.AddressOfPlace))
               .ForMember(x => x.Number, y => y.MapFrom(u => u.Number))
               .ForMember(x => x.Floor, y => y.MapFrom(u => u.Floor))
               .ForMember(x => x.Description, y => y.MapFrom(u => u.Description))
               .ForMember(x => x.Latitude, y => y.MapFrom(u => u.Latitude))
               .ForMember(x => x.Longitude, y => y.MapFrom(u => u.Longitude))
               .ForMember(x => x.City, y => y.MapFrom(u => new ResponseCityDTO
                                                               {
                                                                   Id = u.Id,
                                                                   Name = u.City.Name,
                                                                   ZipCode = u.City.ZipCode
                                                               }));
        }
    }
}
