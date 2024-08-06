using BookATable.Application;
using BookATable.Implementation.Logging.UseCases;
using BookATable.Implementation;
using System.IdentityModel.Tokens.Jwt;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.Implementation.UseCases.Commands.Users;
using BookATable.Implementation.Validators;
using BookATable.Application.Email;
using BookATable.Implementation.Email;
using BookATable.Application.UseCases.Queries.Users;
using BookATable.Implementation.UseCases.Queries.Users;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.Implementation.UseCases.Commands.MealCategories;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Implementation.UseCases.Queries.MealCategories;
using BookATable.Application.UseCases.Commands.Cities;
using BookATable.Implementation.UseCases.Commands.Cities;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Implementation.UseCases.Queries.Cities;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.Implementation.UseCases.Commands.Addresses;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Implementation.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Commands.RestaurantTypes;
using BookATable.Implementation.UseCases.Commands.RestaurantTypes;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.Implementation.UseCases.Queries.RestaurantTypes;
using BookATable.Application.UseCases.Commands.Appendices;
using BookATable.Implementation.UseCases.Commands.Appendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using BookATable.Application.UseCases.Queries.Appendices;
using BookATable.Implementation.UseCases.Queries.Appendices;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.Implementation.UseCases.Commands.Restaurants;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.Implementation.UseCases.Queries.Restaurants;
using BookATable.Application.UseCases.Commands.MealCategoryRestaurants;
using BookATable.Implementation.UseCases.Commands.MealCategoryRestaurants;
using BookATable.Implementation.UseCases.Queries.MealCategoryRestaurants;
using BookATable.Application.UseCases.Queries.MealCategoryRestaurants;
using BookATable.Application.UseCases.Commands.AppendiceRestaurants;
using BookATable.Implementation.UseCases.Commands.AppendiceRestaurants;
using BookATable.Application.UseCases.Queries.AppendiceRestaurants;
using BookATable.Implementation.UseCases.Queries.AppendiceRestaurants;
using BookATable.Application.UseCases.Commands.Dish;
using BookATable.Implementation.UseCases.Commands.Dish;
using BookATable.Application.UseCases.Queries.Dish;
using BookATable.Implementation.UseCases.Queries.Dish;
using BookATable.Application.UseCases.Commands.Ratings;
using BookATable.Implementation.UseCases.Commands.Ratings;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.Implementation.UseCases.Queries.Ratings;
using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.Implementation.UseCases.Commands.Reservations;
using BookATable.Application.UseCases.Queries.Reservations;
using BookATable.Implementation.UseCases.Queries.Reservations;
using BookATable.Application.UseCases.Commands.ReservationAppendices;
using BookATable.Implementation.UseCases.Commands.ReservationAppendices;
using BookATable.Application.UseCases.Queries.ReservationAppendices;
using BookATable.Implementation.UseCases.Queries.ReservationAppendices;
using BookATable.Implementation.Profiles;

namespace BookATable.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<ActivateAccountValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<AdminUpdateUserValidator>();
            services.AddTransient<MealCategoryValidator>();
            services.AddTransient<CreateCityValidator>();
            services.AddTransient<UpdateCityValidator>();
            services.AddTransient<CreateAddressValidator>();
            services.AddTransient<UpdateAddressValidator>();
            services.AddTransient<RestaurantTypeValidator>();
            services.AddTransient<UpdateMealCategoryValidator>();
            services.AddTransient<UpdateRestaurantTypeValidator>();
            services.AddTransient<CreateAppendiceValidator>();
            services.AddTransient<UpdateAppendiceValidator>();
            services.AddTransient<CreateRestaurantValidator>();
            services.AddTransient<UpdateRestaurantValidator>();
            services.AddTransient<AdminUpdateRestourantValidator>();
            services.AddTransient<MealCategoryRestaurantValidator>();
            services.AddTransient<AppendiceRestaurantValidator>();
            services.AddTransient<CreateDishValidator>();
            services.AddTransient<CreateRatingValidator>();
            services.AddTransient<CreateReservationValidator>();
            services.AddTransient<CreateReservationAppendiceValidator>();



            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IEmailSender, SMTPEmailSender>();
            services.AddTransient<IActivateAccountCommand, EfActivateAccountCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IAdminDeleteUserCommand, EfAdminDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IAdminUpdateUserCommand, EfAdminUpdateUserCommand>();
            services.AddTransient<IIsActivateUserQuery, EfIsActivateUserQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<ICreateMealCategoryCommand, EfCreateMealCategoryCommand>();
            services.AddTransient<IUpdateMealCategoryCommand, EfUpdateMealCategoryCommand>();
            services.AddTransient<IDeleteMealCategoryCommand, EfDeleteMealCategoryCommand>();
            services.AddTransient<IGetMealCategoryQuery, EfGetMealCategoryQuery>();
            services.AddTransient<IGetMealCategoriesQuery, EfGetMealCategoriesQuery>();
            services.AddTransient<ICreateCityCommand, EfCreateCityCommand>();
            services.AddTransient<IUpdateCityCommand, EfUpdateCityCommand>();
            services.AddTransient<IGetCityQuery, EfGetCityQuery>();
            services.AddTransient<IGetCitiesQuery, EfGetCitiesQuery>();
            services.AddTransient<IDeleteCityCommand, EfDeleteCityCommand>();
            services.AddTransient<ICreateAddressCommand, EfCreateAddressCommand>();
            services.AddTransient<IUpdateAddressCommand, EfUpdateAddressCommand>();
            services.AddTransient<IDeleteAddressCommand, EfDeleteAddressCommand>();
            services.AddTransient<IGetAddressQuery, EfGetAddressQuery>();
            services.AddTransient<IGetAddressesQuery, EfGetAddresesQuery>();
            services.AddTransient<ICreateRestaurantTypeCommand, EfCreateRestaurantTypesCommand>();
            services.AddTransient<IUpdateRestaurantTypeCommand, EfUpdateRestaurantTypeCommand>();
            services.AddTransient<IDeleteRestaurantTypeCommand, EfDeleteRestaurantTypeCommand>();
            services.AddTransient<IGetRestaurantTypeQuery, EfGetRestaurantTypeQuery>();
            services.AddTransient<IGetRestaurantTypesQuery, EfGetRestaurantTypesQuery>();
            services.AddTransient<ICreateAppendiceCommand, EfCreateAppendiceCommand>();
            services.AddTransient<IUpdateAppendiceCommand, EfUpdateAppendiceCommand>();


            services.AddTransient<Func<Context, DbSet<Appendice>>>(provider => context => context.Set<Appendice>());
            services.AddTransient<Func<Context, DbSet<MealCategory>>>(provider => context => context.Set<MealCategory>());
            services.AddTransient<Func<Context, DbSet<RestaurantType>>>(provider => context => context.Set<RestaurantType>());

            services.AddTransient<Func<Appendice, string>>(provider => entity => entity.Name);
            services.AddTransient<Func<MealCategory, string>>(provider => entity => entity.Name);
            services.AddTransient<Func<RestaurantType, string>>(provider => entity => entity.Name);

            services.AddTransient<Func<Appendice, int>>(provider => entity => entity.Id);
            services.AddTransient<Func<MealCategory, int>>(provider => entity => entity.Id);
            services.AddTransient<Func<RestaurantType, int>>(provider => entity => entity.Id);

            services.AddTransient<IDeleteAppendiceCommand, EfDeleteAppendiceCommand>();
            services.AddTransient<IGetAppendiceQuery, EfGetAppendiceQuery>();
            services.AddTransient<IGetAppendicesQuery, EfGetAppendicesQuery>();
            services.AddTransient<ICreateRestaurantCommand, EfCreateRestaurantCommand>();
            services.AddTransient<IUpdateRestaurantCommand, EfUpdateRestaurantCommand>();
            services.AddTransient<IAdminUpdateRestaurantCommand, EfAdminUpdateRestaurantCommand>();
            services.AddTransient<IDeleteRestaurantCommand, EfDeleteRestaurantCommand>();
            services.AddTransient<IAdminDeleteRestaurantCommand, EfAdminDeleteRestaurantCommand>();
            services.AddTransient<IGetRestaurantQuery, EfGetRestaurantQuery>();
            services.AddTransient<IGetRestaurantsQuery, EfGetRestaurantsQuery>();
            services.AddTransient<ICreateMealCategoryRestaurantCommand, EfCreateMealCategoryRestaurantCommand>();
            services.AddTransient<IUpdateMealCategoryRestaurantCommand, EfUpdateMealCategoryRestaurantCommand>();
            services.AddTransient<IDeleteMealCategoryRestaurantCommand, EfDeleteMealCategoryRestaurantCommand>();
            services.AddTransient<IGetMealCategoryRestaurantQuery, EfGetMealCategoryRestaurantQuery>();
            services.AddTransient<IGetMealCategoryRestaurantsQuery, EfGetMealCategoryRestaurantsQuery>();
            services.AddTransient<ICreateAppendiceRestaurantCommand, EfCreateAppendiceRestaurantCommand>();
            services.AddTransient<IUpdateAppendiceRestaurantCommand, EfUpdateAppendiceRestaurantCommand>();
            services.AddTransient<IDeleteAppendiceRestaurantCommand, EfDeleteAppendiceRestaurantCommand>();
            services.AddTransient<IGetAppendiceRestaurantQuery, EfGetAppendiceRestaurantQuery>();
            services.AddTransient<IGetAppendiceRestaurantsQuery, EfGetAppendiceRestaurantsQuery>();
            services.AddTransient<ICreateDishCommand, EfCreateDishCommand>();
            services.AddTransient<IUpdateDishCommand, EfUpdateDishCommand>();
            services.AddTransient<IDeleteDishCommand, EfDeleteDishCommand>();
            services.AddTransient<IGetDishQuery, EfGetDishQuery>();
            services.AddTransient<IGetDishesQuery, EfGetDishesQuery>();
            services.AddTransient<ICreateRatingCommand, EfCreateRatingCommand>();
            services.AddTransient<IUpdateRatingCommand, EfUpdateRatingCommand>();
            services.AddTransient<IDeleteRatingCommand, EfDeleteRatingCommand>();
            services.AddTransient<IGetRatingQuery, EfGetRatingQuery>();
            services.AddTransient<IGetRatingsQuery, EfGetRatingsQuery>();
            services.AddTransient<ICreateReservationCommand, EfCreateReservationCommand>();
            services.AddTransient<IUpdateReservationCommand, EfUpdateReservationCommand>();
            services.AddTransient<IDeleteReservationCommand, EfDeleteReservationCommand>();
            services.AddTransient<IGetReservationQuery, EfGetReservationQuery>();
            services.AddTransient<IGetReservationsQuery, EfGetReservationsQuery>();
            services.AddTransient<ICreateReservationAppendiceCommand, EfCreateReservationAppendiceCommand>();
            services.AddTransient<IUpdateReservationAppendiceCommand, EfUpdateReservationAppendiceCommand>();
            services.AddTransient<IGetReservationAppendiceQuery, EfGetReservationAppendiceQuery>();
            services.AddTransient<IGetReservationAppendicesQuery, EfGetReservationAppendicesQuery>();



        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }

    }
}

