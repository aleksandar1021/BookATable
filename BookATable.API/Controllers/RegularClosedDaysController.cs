using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.RegularClosedDays;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.Application.UseCases.Queries.RegularClosedDays;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegularClosedDaysController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public RegularClosedDaysController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }



        // GET: api/<RegularClosedDaysController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchRegularClosedDays dto, [FromServices] IGetRegularClosedDaysQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<RegularClosedDaysController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetRegularClosedDayQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<RegularClosedDaysController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRegularClosedDaysDTO dto, [FromServices] ICreateRegularClosedDaysCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<RegularClosedDaysController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int restaurantId, [FromBody] CreateRegularClosedDaysDTO dto, [FromServices] IUpdateRegularClosedDaysCommand cmd)
        {
            dto.RestaurantId = restaurantId;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<RegularClosedDaysController>/5
        [Authorize]
        [HttpDelete("{restaurantId}")]
        public IActionResult Delete(int restaurantId, [FromServices] IDeleteRegularClosedDaysCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, restaurantId);
            return NoContent();
        }
    }
}
