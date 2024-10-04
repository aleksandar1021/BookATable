using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateNamedEntity
    {
        public string Name { get; set; }
    }

    public class UpdateNamedEntity : CreateNamedEntity
    {
        public int Id { get; set; }
    }

    public class ResponseNamedEntityDTO : UpdateNamedEntity
    {

    }

    public class SearchNamedEntityDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateMealCategoryDTO : CreateNamedEntity
    {
        public string Image { get; set; }
    }

    public class UpdateMealCategoryDTO : CreateMealCategoryDTO
    {
        public int Id { get; set; }
    }

    public class ResponseMealCategoryDTO : UpdateMealCategoryDTO
    {
        
    }

    public class ResponseRestaurantTypesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
