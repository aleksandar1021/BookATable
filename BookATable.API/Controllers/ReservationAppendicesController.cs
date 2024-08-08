using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.ReservationAppendices;
using BookATable.Application.UseCases.Queries.AppendiceRestaurants;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.Application.UseCases.Queries.ReservationAppendices;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAppendicesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public ReservationAppendicesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<ReservationAppendicesController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchReservationAppendiceDTO dto, [FromServices] IGetReservationAppendicesQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<ReservationAppendicesController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetReservationAppendiceQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<ReservationAppendicesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationAppendiceDTO dto, [FromServices] ICreateReservationAppendiceCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<ReservationAppendicesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateReservationAppendiceDTO dto, [FromServices] IUpdateReservationAppendiceCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
