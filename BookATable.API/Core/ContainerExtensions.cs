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
            services.AddTransient<NamedEntityValidator>();
            services.AddTransient<CreateCityValidator>();
            services.AddTransient<UpdateCityValidator>();



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

