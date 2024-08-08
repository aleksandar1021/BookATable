using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.SpecificClosedDays;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.Application.UseCases.Queries.SpecificClosedDays;
using BookATable.Implementation;
using BookATable.Implementation.UseCases.Queries.Saved;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificClosedDaysController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public SpecificClosedDaysController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<SpecificClosedDaysController>
        [HttpGet]
        public IActionResult Search([FromQuery] SearchSpecificClosedDays search, [FromServices] IGetSpecificClosedDaysQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));

        // GET api/<SpecificClosedDaysController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetSpecificClosedDayQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<SpecificClosedDaysController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateSpecificClosedDaysDTO dto, [FromServices] ICreateSpecificClosedDaysCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<SpecificClosedDaysController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSpecificClosedDaysDTO dto, [FromServices] IUpdateSpecificClosedDaysCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<SpecificClosedDaysController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecificClosedDaysCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
