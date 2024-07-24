using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateCityDTO
    {
        public string Name { get; set; }
        public int ZipCode { get; set; }
    }

    public class UpdateCityDTO : CreateCityDTO
    {
        public int Id { get; set; }
    }

    public class SearchCityDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ZipCode { get; set; }
    }

    public class ResponseCityDTO : UpdateCityDTO
    {

    }
}
