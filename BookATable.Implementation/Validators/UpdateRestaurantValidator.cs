using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class UpdateRestaurantValidator : BaseRestaurantValidator<CreateRestaurantDTO>
    {
        public UpdateRestaurantValidator(Context ctx, IApplicationActor actor) : base(ctx)
        {
            
        }
    }
}
