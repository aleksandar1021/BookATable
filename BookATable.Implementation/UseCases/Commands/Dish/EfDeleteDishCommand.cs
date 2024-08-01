using BookATable.Application.UseCases.Commands.Dish;
using BookATable.DataAccess;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Dish
{
    public class EfDeleteDishCommand : EfUseCase, IDeleteDishCommand
    {
        public EfDeleteDishCommand(Context context) : base(context)
        {
        }

        public int Id => 53;

        public string Name => "Delete dish";

        public void Execute(int data)
        {
            Domain.Tables.Dish dish = Context.Dishs.FirstOrDefault(x => x.Id == data);

            if (dish == null || !dish.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.Dish), data);
            }

            dish.IsActive = false;

            Context.SaveChanges();
        }
    }
}
