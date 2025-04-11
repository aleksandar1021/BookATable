using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Address : Entity
    {
        public int CityId { get; set; }
        public string Place { get; set; }
        public string AddressOfPlace { get; set; }
        public string Number { get; set; }
        public int? Floor { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}
