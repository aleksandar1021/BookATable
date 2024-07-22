using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Appendice : NamedEntity
    {
        public virtual ICollection<AppendiceRestaurant> AppendiceRestaurants { get; set; } = new HashSet<AppendiceRestaurant>();
        public virtual ICollection<ReservationAppendice> ReservationAppendices { get; set; } = new HashSet<ReservationAppendice>();
    }
}
