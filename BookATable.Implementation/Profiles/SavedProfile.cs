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
    public class SavedProfile : Profile
    {
        public SavedProfile()
        {
            CreateMap<Saved, ResponseSavedDTO>()
              .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
              .ForMember(x => x.RestaurantId, y => y.MapFrom(u => u.RestaurantId))
              .ForMember(x => x.UserId, y => y.MapFrom(u => u.UserId))
              .ForMember(x => x.User, y => y.MapFrom(u => new ResponseBaseUser
              {
                  Id = u.Id,
                  Email = u.User.Email,
                  FirstName = u.User.FirstName,
                  LastName = u.User.LastName
              }))
              .ForMember(x => x.Restaurant, y => y.MapFrom(u => new BaseResponseRestaurantDTO
              {
                  Id = u.Restaurant.Id,
                  Name = u.Restaurant.Name,
                  UserId = u.UserId,
                  Description = u.Restaurant.Description,
                  Image = u.Restaurant.RestaurantImages
                                        .Where(x => x.IsPrimary == true)
                                        .Select(x => x.Path)
                                        .FirstOrDefault(),
             
        }));
        }
    }
}
