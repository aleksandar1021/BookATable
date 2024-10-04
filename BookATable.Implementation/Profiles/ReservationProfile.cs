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
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ResponseReservationDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.UserId, y => y.MapFrom(u => u.UserId))
                .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId))
                .ForMember(x => x.NumberOfGuests, y => y.MapFrom(u => u.NumberOfGuests))
                .ForMember(x => x.Time, y => y.MapFrom(u => u.Time))
                .ForMember(x => x.Note, y => y.MapFrom(u => u.Note))
                .ForMember(x => x.Date, y => y.MapFrom(u => u.Date))
                .ForMember(x => x.IsAccepted, y => y.MapFrom(u => u.IsAccepted))
                .ForMember(x => x.IsRealised, y => y.MapFrom(u => u.IsRealised))
                .ForMember(x => x.Code, y => y.MapFrom(u => u.ReservationCode))

                .ForMember(x => x.User, y => y.MapFrom(u => new ResponseBaseUser
                {
                    Id = u.User.Id,
                    FirstName = u.User.FirstName,
                    LastName = u.User.LastName,
                    Email = u.User.Email
                }))
                .ForMember(x => x.Appendices, y => y.MapFrom(p => p.ReservationAppendices
                                                 .Where(r => r.IsActive)
                                                 .Select(x => new ResponseNamedEntityDTO
                                                 {
                                                     Id = x.Appendice.Id,
                                                     Name = x.Appendice.Name
                                                 })))
                .ForMember(x => x.Restaurant, y => y.MapFrom(u => new BaseResponseRestaurantDTO
                {
                    Id = u.Restaurant.Id,
                    Name = u.Restaurant.Name,
                    UserId = u.Restaurant.UserId,
                    Description = u.Restaurant.Description,
                    Image = u.Restaurant.RestaurantImages
                                        .Where(x => x.IsPrimary == true) 
                                        .Select(x => x.Path)
                                        .FirstOrDefault(),
                                        }));
        }
    }
}
