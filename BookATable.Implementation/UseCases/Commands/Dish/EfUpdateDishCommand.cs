using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Dish;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Dish
{
    public class EfUpdateDishCommand : EfUseCase, IUpdateDishCommand
    {
        private CreateDishValidator _validator;
        private IApplicationActor _actor;
        public EfUpdateDishCommand(Context context, CreateDishValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 52;

        public string Name => "Update dish";

        public void Execute(UpdateDishDTO data)
        {
            var targetRestaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.RestaurantId);

            if (targetRestaurant.UserId != _actor.Id)
            {
                throw new UnauthorizedAccessException();
            }


            Domain.Tables.Dish dish = Context.Dishs.FirstOrDefault(x => x.Id == data.Id);

            if(dish == null || !dish.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.Dish), data.Id);
            }

            _validator.ValidateAndThrow(data);

            dish.Name = data.Name;
            dish.Description = data.Description;
            dish.Price = data.Price;
            dish.RestaurantId = data.RestaurantId;
            dish.UpdatedAt = DateTime.UtcNow;

            if (!data.Image.IsNullOrEmpty())
            {
                var oldImagePath = Path.Combine("wwwroot", "dishPhotos", dish.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            

            if (!data.Image.IsNullOrEmpty())
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "dishPhotos", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);
                dish.Image = data.Image;
            }

            Context.SaveChanges();
        }
    }
}
