using BookATable.Application.DTO;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateAddressValidator : BaseAddressValidator<CreateAddressDTO>
    {
        public CreateAddressValidator(Context ctx) : base(ctx)
        {
        }
    }

    public class UpdateAddressValidator : CreateAddressValidator
    {
        public UpdateAddressValidator(Context ctx) : base(ctx)
        {

        }
    }
}
