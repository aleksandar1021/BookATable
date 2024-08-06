using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.Application.UseCases.Queries.Reservations;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public ReservationsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<ReservationsController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchReservationDTO dto, [FromServices] IGetReservationsQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetReservationQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<ReservationsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationDTO dto, [FromServices] ICreateReservationCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<ReservationsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateReservationDTO dto, [FromServices] IUpdateReservationCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<ReservationsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteReservationCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
