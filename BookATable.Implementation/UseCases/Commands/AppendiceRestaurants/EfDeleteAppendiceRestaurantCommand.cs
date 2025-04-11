using BookATable.Application.UseCases.Commands.AppendiceRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.AppendiceRestaurants
{
    public class EfDeleteAppendiceRestaurantCommand : EfUseCase, IDeleteAppendiceRestaurantCommand
    {
        public EfDeleteAppendiceRestaurantCommand(Context context) : base(context)
        {
        }

        public int Id => 48;

        public string Name => "Delete restaurant appendice";

        public void Execute(int data)
        {
            AppendiceRestaurant ar = Context.AppendiceRestaurants.FirstOrDefault(x => x.Id == data);

            if (ar == null || !ar.IsActive)
            {
                throw new NotFoundException(nameof(AppendiceRestaurant), Id);
            }

            ar.IsActive = false;
            Context.SaveChanges();
        }
    }
}
