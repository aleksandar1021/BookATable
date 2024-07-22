using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class AppendiceRestaurant : Entity
    {
        public int AppendiceId { get; set; }
        public int RestaurantId { get; set; }

        public virtual Appendice Appendice { get; set; }    
        public virtual Restaurant Restaurant { get; set; }  
    }
}
