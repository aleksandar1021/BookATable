using BookATable.Application;
using BookATable.Implementation.Logging.UseCases;
using BookATable.Implementation;
using System.IdentityModel.Tokens.Jwt;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.Implementation.UseCases.Commands.Users;
using BookATable.Implementation.Validators;
using BookATable.Application.Email;
using BookATable.Implementation.Email;

namespace BookATable.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<ActivateAccountValidator>();
            services.AddTransient<UpdateUserValidator>();

            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IEmailSender, SMTPEmailSender>();
            services.AddTransient<IActivateAccountCommand, EfActivateAccountCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IAdminDeleteUserCommand, EfAdminDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
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

