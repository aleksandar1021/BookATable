using Azure;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookATable.Implementation.UseCases.Queries.Restaurants
{
    public class EfGetTop3RestaurantsQuery : EfUseCase, IGetTop3Restaurants
    {
        private IApplicationActor _actor;
        public EfGetTop3RestaurantsQuery(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 96;

        public string Name => "Get top 3 restaurants";

        public PagedResponse<ResponseRestaurantForUserDTO> Execute(SearchRestaurantDTO data)
        {

            var query = Context.Restaurants
                                .Where(x => x.IsActive && x.IsActivated)
                                .AsQueryable();

            int totalCountOfLogs = query.Count();
            int perPage = data.PerPage.HasValue ? (int)Math.Abs((double)data.PerPage) : 5;
            int page = data.Page.HasValue ? (int)Math.Abs((double)data.Page) : 1;
            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

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
                    PrimaryImagePath = x.RestaurantImages
                                        .Where(img => img.IsPrimary == true)
                                        .Select(img => img.Path)
                                        .FirstOrDefault(),
                    
                    IsSaved = x.Saved.Any(s => s.RestaurantId == x.Id && _actor.Id == s.UserId && s.IsActive),
                    Rate = Math.Round(x.Ratings.Where(r => r.IsActive)
                                 .Average(r => (decimal?)r.Rate) ?? 0,
                          1),
                    NumberOfRates = x.Ratings.Count(r => r.IsActive),
              
                }).OrderByDescending(x => x.Rate).ToList(),
                PerPage = perPage,
                TotalCount = totalCountOfLogs,

            };
        }
    }
}
