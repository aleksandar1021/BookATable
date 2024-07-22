using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Reservation : Entity
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int NumberOfGuests { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public string Note { get; set; }
        public DateOnly Date { get; set; }
        public int ReservationNumber { get; set; }
        public string ReservationCode { get; set; }
        public bool IsRealised { get; set; }


        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<ReservationAppendice> ReservationAppendices { get; set; } = new HashSet<ReservationAppendice>();


    }
}
