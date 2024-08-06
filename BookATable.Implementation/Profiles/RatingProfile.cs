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
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, ResponseRatingDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.Rate, y => y.MapFrom(u => u.Rate))
                .ForMember(x => x.UserId, y => y.MapFrom(u => u.UserId))
                .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId))
                .ForMember(x => x.Message, y => y.MapFrom(u => u.Message))
                .ForMember(x => x.User, y => y.MapFrom(u => new ResponseBaseUser
                {
                    Id = u.User.Id,
                    FirstName = u.User.FirstName,
                    LastName = u.User.LastName,
                    Email = u.User.Email
                }))
                .ForMember(x => x.Restaurant, y => y.MapFrom(u => new BaseResponseRestaurantDTO
                {
                    Id = u.Restaurant.Id,
                    Name = u.Restaurant.Name,
                    UserId = u.Restaurant.UserId,
                    Description = u.Restaurant.Description
                }));

        }
    }
}
