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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResultDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.Email, y => y.MapFrom(u => u.Email))
                .ForMember(x => x.FirstName, y => y.MapFrom(u => u.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(u => u.LastName))
                .ForMember(x => x.Image, y => y.MapFrom(u => u.Image))
                .ForMember(x => x.IsActivatedUser, y => y.MapFrom(u => u.IsActivatedUser))
                .ForMember(x => x.Restaurants, y => y.MapFrom(p => p.Restaurants
                                                     .Where(r => r.IsActive)
                                                     .Select(x => new RestaurantsDTO
                {
                    Id = x.Id,
                    WorkFromHour = x.WorkFromHour,
                    WorkUntilHour = x.WorkUntilHour,
                    WorkFromMinute = x.WorkFromMinute,
                    WorkUntilMinute = x.WorkUntilMinute,
                    MaxNumberOfGuests = x.MaxNumberOfGuests,
                    Description = x.Description,
                    TimeInterval = x.TimeInterval,
                    UserId = x.UserId,
                    Address = new AddressDTO
                    {
                        Id = x.Address.Id,
                        City = x.Address.City.Name,
                        Floor = x.Address.Floor,
                        Number = x.Address.Number,
                        Place = x.Address.Place
                    }
                })))
                .ForMember(x => x.Ratings, y => y.MapFrom(p => p.Ratings
                                                 .Where(r => r.IsActive)
                                                 .Select(x => new RatingDTO
                {
                    Id = x.Id,
                    Rate = x.Rate,
                    RestaurantId = x.RestaurantId,
                    UserId = x.UserId
                })))
                .ForMember(x => x.Reservations, y => y.MapFrom(p => p.Reservations
                                                      .Where(r => r.IsActive)
                                                      .Select(x => new ReservationDTO
                {
                    Id = x.Id,
                    NumberOfGuests = x.NumberOfGuests,
                    IsRealised = x.IsRealised,
                    Date = x.Date,
                    Note = x.Note,
                    ReservationCode = x.ReservationCode,
                    RestaurantId = x.RestaurantId,
                    TimeHour = x.TimeHour,
                    TimeMinute = x.TimeMinute,
                    UserId = x.UserId
                })));







        }
    }
}
