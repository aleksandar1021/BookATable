using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.AppendiceRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.AppendiceRestaurants
{
    public class EfUpdateAppendiceRestaurantCommand : EfUseCase, IUpdateAppendiceRestaurantCommand
    {
        private AppendiceRestaurantValidator _validator;
        public EfUpdateAppendiceRestaurantCommand(Context context, AppendiceRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 47;

        public string Name => "Update restaurant appendice";

        public void Execute(UpdateAppendiceRestaurantDTO data)
        {
            AppendiceRestaurant ar = Context.AppendiceRestaurants.FirstOrDefault(x => x.Id == data.Id);

            if(ar == null || !ar.IsActive)
            {
                throw new NotFoundException(nameof(AppendiceRestaurant), data.Id);
            }

            ar.RestaurantId = data.RestaurantId;
            ar.AppendiceId = data.AppendiceId;
            ar.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
