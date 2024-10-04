using AutoMapper;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Restaurants
{
    public class EfGetRestaurantsForUserQuery : EfUseCase, IGetRestaurantsForUserQuery
    {
        private IApplicationActor _actor;
        public EfGetRestaurantsForUserQuery(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 73;

        public string Name => "User search restaurants";

        public PagedResponse<ResponseRestaurantForUserDTO> Execute(SearchRestaurantDTO data)
        {
            var query = Context.Restaurants
                                .Where(x => x.IsActive && x.IsActivated)
                                .AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id.Value);
            }
            //to
            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            if (data.WorkFromHour.HasValue)
            {
                query = query.Where(x => x.WorkFromHour == data.WorkFromHour);
            }

            if (data.WorkUntilHour.HasValue)
            {
                query = query.Where(x => x.WorkUntilHour == data.WorkUntilHour.Value);
            }

            if (data.WorkFromMinute.HasValue)
            {
                query = query.Where(x => x.WorkFromMinute == data.WorkFromMinute.Value);
            }

            if (data.WorkUntilMinute.HasValue)
            {
                query = query.Where(x => x.WorkUntilMinute == data.WorkUntilMinute.Value);
            }



            //to
            if (data.MealCategoryId.HasValue)
            {
                query = query.Where(r => Context.MealCategoryRestaurants
                    .Any(mcr => mcr.RestaurantId == r.Id && mcr.MealCategoryId == data.MealCategoryId.Value));
            }

            if (data.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == data.UserId.Value);
            }

            //to
            if (data.RestaurantTypeId.HasValue)
            {
                query = query.Where(x => x.RestaurantTypeId == data.RestaurantTypeId.Value);
            }

            if (!string.IsNullOrEmpty(data.Description))
            {
                query = query.Where(x => x.Description.Contains(data.Description));
            }

            if (data.MaxNumberOfGuests.HasValue)
            {
                query = query.Where(x => x.MaxNumberOfGuests == data.MaxNumberOfGuests.Value);
            }

            if (data.TimeInterval.HasValue)
            {
                query = query.Where(x => x.TimeInterval == data.TimeInterval.Value);
            }

            if (data.IsActivated.HasValue)
            {
                query = query.Where(x => x.IsActivated == data.IsActivated.Value);
            }

            if (!data.Sorts.Any())
            {
                query = query.OrderBy(x => x.Ratings.Count);
            }
            else
            {
                if (data.Sorts.Any(x => x.SortProperty == "name"))
                {
                    if (data.Sorts.FirstOrDefault(x => x.SortProperty == "name").Direction == SortDirection.Asc)
                    {
                        query = query.OrderBy(x => x.Name);
                        
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.Name);
                    }
                }

                if (data.Sorts.Any(x => x.SortProperty == "created"))
                {
                    if (data.Sorts.FirstOrDefault(x => x.SortProperty == "created").Direction == SortDirection.Asc)
                    {
                        query = query.OrderBy(x => x.CreatedAt);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.CreatedAt);
                    }
                }
            }




            int totalCountOfLogs = query.Count();
            int perPage = data.PerPage.HasValue ? (int)Math.Abs((double)data.PerPage) : 5;
            int page = data.Page.HasValue ? (int)Math.Abs((double)data.Page) : 1;
            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            int id = _actor.Id;


            
            return new PagedResponse<ResponseRestaurantForUserDTO>
            {
                CurrentPage = page,
                Data = query.Where(x => x.IsActivated && x.IsActive).Select(x => new ResponseRestaurantForUserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    WorkFromHour = x.WorkFromHour,
                    WorkUntilHour = x.WorkUntilHour,
                    WorkFromMinute = x.WorkFromMinute,
                    WorkUntilMinute = x.WorkUntilMinute,
                    UserId = x.UserId,
                    RestaurantTypeId = x.RestaurantTypeId,
                    Description = x.Description,
                    MaxNumberOfGuests = x.MaxNumberOfGuests,
                    TimeInterval = x.TimeInterval,
                    IsActivated = x.IsActivated,
                    CreatedAt = x.CreatedAt,
                    RestaurantType = x.RestaurantType.Name,
                    User = new ResponseBaseUser
                    {
                        Email = x.User.Email,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Id = x.User.Id,
                        Image = x.User.Image
                    },
                    //Images = x.RestaurantImages.Select(img => img.Path),
                    PrimaryImagePath = x.RestaurantImages
                                        .Where(img => img.IsPrimary == true)
                                        .Select(img => img.Path)
                                        .FirstOrDefault(),
                    //Reservations = x.Reservations
                    //    .Where(r => r.IsActive)
                    //    .Select(r => new ReservationDTO
                    //    {
                    //        Id = r.Id,
                    //        NumberOfGuests = r.NumberOfGuests,
                    //        IsRealised = r.IsRealised,
                    //        Date = r.Date,
                    //        Note = r.Note,
                    //        ReservationCode = r.ReservationCode,
                    //        RestaurantId = r.RestaurantId,
                    //        Time = r.Time,
                    //        UserId = r.UserId
                    //    }),
                    //Ratings = x.Ratings
                    //    .Where(r => r.IsActive)
                    //    .Select(r => new RatingDTO
                    //    {
                    //        Id = r.Id,
                    //        Rate = r.Rate,
                    //        RestaurantId = r.RestaurantId,
                    //        UserId = r.UserId
                    //    }),
                    //MealCategories = x.MealCategoryRestaurants
                    //    .Where(m => m.IsActive)
                    //    .Select(m => new ResponseMealCategoryForRestaurant
                    //    {
                    //        Id = m.Id,
                    //        Name = m.MealCategory.Name
                    //    }),
                    //RestaurantImages = x.RestaurantImages
                    //    .Where(img => img.IsActive)
                    //    .Select(img => new ResponseRestaurantImageForRestaurant
                    //    {
                    //        Id = img.Id,
                    //        Path = img.Path,
                    //        RestaurantId = img.RestaurantId,
                    //        IsPrimary = img.IsPrimary
                    //    }),
                    //Dishes = x.Dishs
                    //    .Where(d => d.IsActive)
                    //    .Select(d => new ResponseDishForRestaurant
                    //    {
                    //        Id = d.Id,
                    //        Name = d.Name,
                    //        RestaurantId = d.RestaurantId,
                    //        Description = d.Description,
                    //        Price = d.Price
                    //    }),
                    //AppendiceRestaurants = x.AppendiceRestaurants
                    //    .Where(a => a.IsActive)
                    //    .Select(a => new ResponseAppendiceRestaurantForRestaurant
                    //    {
                    //        Id = a.Id,
                    //        Name = a.Appendice.Name
                    //    }),
                    IsSaved = x.Saved.Any(s => s.RestaurantId == x.Id && _actor.Id == s.UserId && s.IsActive),
                    Rate = Math.Round(x.Ratings.Where(r => r.IsActive)
                                 .Average(r => (decimal?)r.Rate) ?? 0,
                          1),
                    NumberOfRates = x.Ratings.Count(r => r.IsActive),
                    //RegularColsedDays = x.RegularClosedDays.Select(x => x.DayOfWeek.ToString()).ToList(),
                    //SpecificColsedDays = x.SpecificClosedDays.Select(sc => new SpeceificClosedDays
                    //                                     {
                    //                                         ClosedFrom = sc.ClosedFrom,
                    //                                         ClosedTo = sc.ClosedTo
                    //                                     }).Where(sc => sc.ClosedFrom > DateOnly.FromDateTime(DateTime.Now))
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCountOfLogs,

            };

        }
    }
}
