using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.AppendiceRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.AppendiceRestaurants
{
    public class EfCreateAppendiceRestaurantCommand : EfUseCase, ICreateAppendiceRestaurantCommand
    {
        private AppendiceRestaurantValidator _validator;
        public EfCreateAppendiceRestaurantCommand(Context context, AppendiceRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 46;

        public string Name => "Create restaurant appendice";

        public void Execute(CreateAppendiceRestaurantDTO data)
        {
            _validator.ValidateAndThrow(data);

            AppendiceRestaurant ar = new AppendiceRestaurant
            {
                AppendiceId = data.AppendiceId,
                RestaurantId = data.RestaurantId
            };

            Context.AppendiceRestaurants.Add(ar);
            Context.SaveChanges();
        }
    }
}
