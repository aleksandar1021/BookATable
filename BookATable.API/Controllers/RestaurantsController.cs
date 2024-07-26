using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public RestaurantsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<RestaurantsController>
        [HttpGet]
        public IActionResult Search([FromQuery] SearchRestaurantDTO search, [FromServices] IGetRestaurantsQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));

        // GET api/<RestaurantsController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetRestaurantQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<RestaurantsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRestaurantDTO dto, [FromServices] ICreateRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<RestaurantsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRestaurantDTO dto, [FromServices] IUpdateRestaurantCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/Admin")]
        public IActionResult PutAdmin(int id, [FromBody] UpdateRestaurantDTO dto, [FromServices] IAdminUpdateRestaurantCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<RestaurantsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }

        // DELETE api/<RestaurantsController>/5
        [Authorize]
        [HttpDelete("{id}/Admin")]
        public IActionResult DeleteAdmin(int id, [FromServices] IAdminDeleteRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
