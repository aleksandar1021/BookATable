using BookATable.Application.UseCases.Commands.RestaurantTypes;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RestaurantTypes
{
    public class EfDeleteRestaurantTypeCommand : EfUseCase, IDeleteRestaurantTypeCommand
    {
        public EfDeleteRestaurantTypeCommand(Context context) : base(context)
        {
        }

        public int Id => 26;

        public string Name => "Delete restaurant type";

        public void Execute(int data)
        {
            RestaurantType rt = Context.RestaurantTypes.Include(x => x.Restaurants)
                                                       .FirstOrDefault(x => x.IsActive);

            if (rt == null || !rt.IsActive)
            {
                throw new NotFoundException(nameof(RestaurantType), data);
            }

            rt.IsActive = false;
            Context.SaveChanges();
        }
    }
}
