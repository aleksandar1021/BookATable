using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class DishImage : Entity
    {
        public int DishId { get; set; }
        public string Path { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
