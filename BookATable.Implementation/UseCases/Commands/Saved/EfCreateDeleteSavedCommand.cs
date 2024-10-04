using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Saved;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Saved
{
    public class EfCreateDeleteSavedCommand : EfUseCase, ICreateDeleteSavedCommand
    {
        private SavedValidator _validator;
        private IApplicationActor _actor;

        public EfCreateDeleteSavedCommand(Context context, SavedValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 70;

        public string Name => "Create or delete saved";

        public void Execute(CreateSavedDTO data)
        {
            _validator.ValidateAndThrow(data);
            int restaurantId = data.RestaurantId;
            int userId = _actor.Id;

            Domain.Tables.Saved saved = Context.Saved.Where(x => x.UserId == userId && x.RestaurantId == restaurantId).FirstOrDefault();

            if(saved == null)
            {
                Domain.Tables.Saved newSaved = new Domain.Tables.Saved
                {
                    RestaurantId = restaurantId,
                    UserId = userId
                };
                Context.Saved.Add(newSaved);
            }
            else if(!saved.IsActive)
            {
                saved.IsActive = true;
            }
            else
            {
                saved.IsActive = false;
            }

            Context.SaveChanges();
        }
    }
}
