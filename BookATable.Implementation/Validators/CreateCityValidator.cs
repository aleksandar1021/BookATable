using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateCityValidator : BaseCityValidator<CreateCityDTO>
    {
    }

    public class UpdateCityValidator : CreateCityValidator
    {
    }
}
