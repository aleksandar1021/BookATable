using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using BookATable.Application.UseCases.Commands.Dish;
using BookATable.DataAccess;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookATable.Application;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace BookATable.Implementation.UseCases.Commands.Dish
{
    public class EfCreateDishCommand : EfUseCase, ICreateDishCommand
    {
        private CreateDishValidator _validator;
        private IApplicationActor __actor;

        public EfCreateDishCommand(Context context, CreateDishValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            __actor = actor;
        }

        public int Id => 51;

        public string Name => "Create dish";

        public void Execute(CreateDishDTO data)
        {
            var targetRestaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.RestaurantId);

            if(targetRestaurant.UserId != __actor.Id)
            {
                throw new UnauthorizedAccessException();
            }

            _validator.ValidateAndThrow(data);

            Domain.Tables.Dish dish = new Domain.Tables.Dish
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                RestaurantId = data.RestaurantId
                
            };

            if (!data.Image.IsNullOrEmpty())
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "dishPhotos", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);
                dish.Image = data.Image;
            }

            Context.Dishs.Add(dish);
            Context.SaveChanges();
        }
    }
}
