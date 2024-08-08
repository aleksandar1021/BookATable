using BookATable.Application;
using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfRealiseReservationCommand : EfUseCase, IRealiseReservationCommand
    {
        private IApplicationActor _actor;
        public EfRealiseReservationCommand(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 89;

        public string Name => "Realise reservation";

        public void Execute(int data)
        {
            Reservation targetReservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (targetReservation == null || !targetReservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            Restaurant targetRestaurant = targetReservation.Restaurant;

            if (targetRestaurant.UserId != _actor.Id)
            {
                throw new ConflictException("You do not have privileges for this action");
            }

            targetReservation.IsRealised = true;
            Context.SaveChanges();
        }
    }
}
