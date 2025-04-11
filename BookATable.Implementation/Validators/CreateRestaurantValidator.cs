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
    public class CreateRestaurantValidator : BaseRestaurantValidator<CreateRestaurantDTO>
    {
        public CreateRestaurantValidator(Context ctx) : base(ctx)
        {
            RuleFor(x => x.Images)
                    .Must(images => images == null || images.Any())
                    .WithMessage("Minimum one image is required if images are provided.")
                    .When(x => x.Images != null)
                    .DependentRules(() =>
                    {
                        RuleForEach(x => x.Images)
                            .Must(fileName =>
                            {
                                var path = Path.Combine("wwwroot", "temp", fileName);
                                return File.Exists(path);
                            })
                            .WithMessage("File does not exist.");
                    });
        }
    }
}
