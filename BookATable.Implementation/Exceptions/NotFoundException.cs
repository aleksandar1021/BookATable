using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityType, int id) :
            base($"Entity of type {entityType} with an id of {id} doesn't exist.")
        {

        }

        public NotFoundException(string entityType, string entry) :
            base($"Entity of type {entityType} with entry: {entry} doesn't exist.")
        {

        }
    }
}
