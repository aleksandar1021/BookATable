using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class ReservationAppendice : Entity
    {
        public int ReservationId { get; set; }
        public int AppendiceId { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Appendice Appendice { get; set; }
    }
}
