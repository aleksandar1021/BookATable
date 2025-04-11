using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.Application.UseCases.Commands.RestaurantTypes;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTypesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public RestaurantTypesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // GET: api/<RestaurantTypesController>
        [HttpGet]
        public IActionResult Search([FromQuery] SearchNamedEntityDTO search, [FromServices] IGetRestaurantTypesQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));

        // GET api/<RestaurantTypesController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetRestaurantTypeQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<RestaurantTypesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateNamedEntity dto, [FromServices] ICreateRestaurantTypeCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<RestaurantTypesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateNamedEntity dto, [FromServices] IUpdateRestaurantTypeCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<RestaurantTypesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantTypeCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
