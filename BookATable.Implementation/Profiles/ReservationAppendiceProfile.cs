using AutoMapper;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Profiles
{
    public class ReservationAppendiceProfile : Profile
    {
        public ReservationAppendiceProfile()
        {
            CreateMap<ReservationAppendice, ResponseReservationAppendiceDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(dest => dest.AppendiceId, opt => opt.MapFrom(src => src.AppendiceId))
            .ForMember(x => x.Appendice, y => y.MapFrom(u => u.Appendice.IsActive ? new ResponseNamedEntityDTO
            {
                Id = u.Appendice.Id,
                Name = u.Appendice.Name
            } : null))
            .ForMember(x => x.Restaurant, y => y.MapFrom(u => u.Reservation.Restaurant.IsActive ? new BaseResponseRestaurantDTO
            {
                Id = u.Reservation.Restaurant.Id,
                Name = u.Reservation.Restaurant.Name,
                UserId = u.Reservation.Restaurant.UserId,
                Description = u.Reservation.Restaurant.Description
            } : null));

            }
        }
}
