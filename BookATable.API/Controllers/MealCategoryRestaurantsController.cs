using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategoryRestaurants;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Queries.MealCategoryRestaurants;
using BookATable.Implementation;
using BookATable.Implementation.UseCases.Queries.MealCategoryRestaurants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealCategoryRestaurantsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public MealCategoryRestaurantsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<MealCategoryRestaurantsController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchMealCategoryRestaurantDTO dto, [FromServices] IGetMealCategoryRestaurantsQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<MealCategoryRestaurantsController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetMealCategoryRestaurantQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<MealCategoryRestaurantsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateMealCategoryRestaurantDTO dto, [FromServices] ICreateMealCategoryRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<MealCategoryRestaurantsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateMealCategoryRestaurantDTO dto, [FromServices] IUpdateMealCategoryRestaurantCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<MealCategoryRestaurantsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteMealCategoryRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
