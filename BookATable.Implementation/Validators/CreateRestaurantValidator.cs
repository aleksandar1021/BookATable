using BookATable.Application.DTO;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateRestaurantValidator : BaseRestaurantValidator<CreateRestaurantDTO>
    {
        public CreateRestaurantValidator(Context ctx) : base(ctx)
        {
        }
    }
}
