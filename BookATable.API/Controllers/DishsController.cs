using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Dish;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Application.UseCases.Queries.Dish;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public DishsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // GET: api/<DishsController>
        [HttpGet]
        public IActionResult Search([FromQuery] SearchDishDTO search, [FromServices] IGetDishesQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));

        // GET api/<DishsController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetDishQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<DishsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateDishDTO dto, [FromServices] ICreateDishCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<DishsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateDishDTO dto, [FromServices] IUpdateDishCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<DishsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteDishCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
