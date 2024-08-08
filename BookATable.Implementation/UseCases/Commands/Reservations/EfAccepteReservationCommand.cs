using BookATable.Application;
using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfAccepteReservationCommand : EfUseCase, IAccepteReservationCommand
    {
        private IApplicationActor _actor;
        public EfAccepteReservationCommand(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 88;

        public string Name => "Accepte reservation";

        public void Execute(int data)
        {
            Reservation targetReservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (targetReservation == null || !targetReservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            Restaurant targetRestaurant = targetReservation.Restaurant;

            if(targetRestaurant.UserId != _actor.Id)
            {
                throw new ConflictException("You do not have privileges for this action");
            }

            targetReservation.IsAccepted = true;
            Context.SaveChanges();

        }
    }
}
