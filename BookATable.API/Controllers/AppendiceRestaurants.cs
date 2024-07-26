using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.AppendiceRestaurants;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Queries.AppendiceRestaurants;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppendiceRestaurants : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public AppendiceRestaurants(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<AppendiceRestaurants>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchAppendiceRestaurantDTO dto, [FromServices] IGetAppendiceRestaurantsQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<AppendiceRestaurants>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetAppendiceRestaurantQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<AppendiceRestaurants>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateAppendiceRestaurantDTO dto, [FromServices] ICreateAppendiceRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<AppendiceRestaurants>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAppendiceRestaurantDTO dto, [FromServices] IUpdateAppendiceRestaurantCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<AppendiceRestaurants>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAppendiceRestaurantCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
