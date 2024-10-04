using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateAddressDTO
    {
        public int CityId { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public int? Floor { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

    }

    public class UpdateAddressDTO : CreateAddressDTO
    {
        public int Id { get; set; }
    }

    public class ResponseAddressDTO : UpdateAddressDTO
    {
        public ResponseCityDTO City { get; set; }
    }

    public class SearchAddressDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? CityId { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public int? Floor { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
