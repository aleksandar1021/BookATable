using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class RegularClosedDays : Entity
    {
        public int RestaurantId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }

    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
