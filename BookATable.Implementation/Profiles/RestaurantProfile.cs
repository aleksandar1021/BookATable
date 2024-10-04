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
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<System.DayOfWeek, string>().ConvertUsing(d => d.ToString());

            CreateMap<SpecificClosedDays, SpeceificClosedDays>()
                .ForMember(dest => dest.ClosedFrom, opt => opt.MapFrom(src => src.ClosedFrom))
                .ForMember(dest => dest.ClosedTo, opt => opt.MapFrom(src => src.ClosedTo));

            CreateMap<Restaurant, ResponseRestaurantDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.Name, y => y.MapFrom(u => u.Name))
                .ForMember(x => x.PrimaryImagePath, y => y.MapFrom(u => u.RestaurantImages
                    .Where(img => img.IsPrimary == true)
                    .Select(img => img.Path)
                    .FirstOrDefault()))
                .ForMember(x => x.RestaurantType, y => y.MapFrom(u => u.RestaurantType.Name))
                .ForMember(x => x.WorkFromHour, y => y.MapFrom(u => u.WorkFromHour))
                .ForMember(x => x.WorkUntilHour, y => y.MapFrom(u => u.WorkUntilHour))
                .ForMember(x => x.WorkFromMinute, y => y.MapFrom(u => u.WorkFromMinute))
                .ForMember(x => x.WorkUntilMinute, y => y.MapFrom(u => u.WorkUntilMinute))
                .ForMember(x => x.AddressId, y => y.MapFrom(u => u.AddressId))
                .ForMember(x => x.UserId, y => y.MapFrom(u => u.UserId))
                .ForMember(x => x.RestaurantTypeId, y => y.MapFrom(u => u.RestaurantTypeId))
                .ForMember(x => x.Description, y => y.MapFrom(u => u.Description))
                .ForMember(x => x.MaxNumberOfGuests, y => y.MapFrom(u => u.MaxNumberOfGuests))
                .ForMember(x => x.TimeInterval, y => y.MapFrom(u => u.TimeInterval))
                .ForMember(x => x.IsActivated, y => y.MapFrom(u => u.IsActivated))
                .ForMember(x => x.RegularClosedDays, y => y.MapFrom(u => u.RegularClosedDays
                    .Select(rcd => (int)rcd.DayOfWeek)
                    .ToList()))
                .ForMember(x => x.SpecificColsedDays, y => y.MapFrom(u => u.SpecificClosedDays
                    .Where(sc => sc.ClosedFrom > DateOnly.FromDateTime(DateTime.Now) && sc.IsActive)
                    .Select(sc => new SpeceificClosedDays
                    {
                        ClosedFrom = sc.ClosedFrom,
                        ClosedTo = sc.ClosedTo
                    })
                ))
                .ForMember(x => x.Ratings, y => y.MapFrom(p => p.Ratings
                    .Where(r => r.IsActive)
                    .Select(x => new RatingDTO
                    {
                        Id = x.Id,
                        Rate = x.Rate,
                        RestaurantId = x.RestaurantId,
                        UserId = x.UserId,
                        Message = x.Message,
                        User = new ResponseBaseUser
                        {
                            Email = x.User.Email,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Image = x.User.Image,
                            Id = x.User.Id
                        }
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
                        Time = x.Time,
                        UserId = x.UserId
                    })))
                .ForMember(x => x.MealCategories, y => y.MapFrom(p => p.MealCategoryRestaurants
                    .Where(r => r.IsActive)
                    .Select(x => new ResponseMealCategoryForRestaurant
                    {
                        Id = x.Id,
                        Name = x.MealCategory.Name
                    })))
                .ForMember(x => x.RestaurantImages, y => y.MapFrom(p => p.RestaurantImages
                    .Where(r => r.IsActive)
                    .Select(x => new ResponseRestaurantImageForRestaurant
                    {
                        Id = x.Id,
                        Path = x.Path,
                        RestaurantId = x.RestaurantId,
                        IsPrimary = x.IsPrimary
                    })))
                .ForMember(x => x.Dishes, y => y.MapFrom(p => p.Dishs
                    .Where(r => r.IsActive)
                    .Select(x => new ResponseDishForRestaurant
                    {
                        Id = x.Id,
                        Name = x.Name,
                        RestaurantId = x.RestaurantId, 
                        Description = x.Description,
                        Price = x.Price,
                        Image = x.Image
                    })))
                .ForMember(x => x.AppendiceRestaurants, y => y.MapFrom(p => p.AppendiceRestaurants
                    .Where(r => r.IsActive)
                    .Select(x => new ResponseAppendiceRestaurantForRestaurant
                    {
                        Id = x.Appendice.Id,
                        Name = x.Appendice.Name,
                    })))
                .ForMember(x => x.User, y => y.MapFrom(p => new ResponseBaseUser
                {
                    Email = p.User.Email,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Image = p.User.Image,
                    Id = p.User.Id
                }))
                .ForMember(x => x.Address, y => y.MapFrom(p => new ResponseAddressDTO
                {
                    Address = p.Address.AddressOfPlace,
                    Place = p.Address.Place,
                    Description = p.Address.Description,
                    Floor = p.Address.Floor,
                    Number = p.Address.Number,
                    City = new ResponseCityDTO
                    {
                        Id = p.Address.City.Id,
                        Name = p.Address.City.Name,
                        ZipCode = p.Address.City.ZipCode
                    }
                }));



            CreateMap<Restaurant, ResponseRestaurantDTOAdmin>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
                .ForMember(x => x.Name, y => y.MapFrom(u => u.Name))
                .ForMember(x => x.PrimaryImagePath, y => y.MapFrom(u => u.RestaurantImages.Where(x => x.IsPrimary == true).Select(img => img.Path).FirstOrDefault()))
                .ForMember(x => x.RestaurantType, y => y.MapFrom(u => u.RestaurantType.Name))
                .ForMember(x => x.WorkFromHour, y => y.MapFrom(u => u.WorkFromHour))
                .ForMember(x => x.WorkUntilHour, y => y.MapFrom(u => u.WorkUntilHour))
                .ForMember(x => x.WorkFromMinute, y => y.MapFrom(u => u.WorkFromMinute))
                .ForMember(x => x.WorkUntilMinute, y => y.MapFrom(u => u.WorkUntilMinute))
                .ForMember(x => x.AddressId, y => y.MapFrom(u => u.AddressId))
                .ForMember(x => x.UserId, y => y.MapFrom(u => u.UserId))
                .ForMember(x => x.RestaurantTypeId, y => y.MapFrom(u => u.RestaurantTypeId))
                .ForMember(x => x.Description, y => y.MapFrom(u => u.Description))
                .ForMember(x => x.MaxNumberOfGuests, y => y.MapFrom(u => u.MaxNumberOfGuests))
                .ForMember(x => x.TimeInterval, y => y.MapFrom(u => u.TimeInterval))
                .ForMember(x => x.IsActivated, y => y.MapFrom(u => u.IsActivated))
                .ForMember(x => x.User, y => y.MapFrom(p => new ResponseBaseUser
                {
                    Email = p.User.Email,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Image = p.User.Image,
                    Id = p.User.Id
                }))
                .ForMember(x => x.Address, y => y.MapFrom(p => new ResponseAddressDTO
                {
                    Address = p.Address.AddressOfPlace,
                    Place = p.Address.Place,
                    Description = p.Address.Description,
                    Floor = p.Address.Floor,
                    Number = p.Address.Number,
                    City = new ResponseCityDTO
                    {
                        Id = p.Address.City.Id,
                        Name = p.Address.City.Name,
                        ZipCode = p.Address.City.ZipCode
                    }
                }));


        }
    }
}
