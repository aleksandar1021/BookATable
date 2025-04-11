using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Appendices;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Queries.Appendices;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppendicesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public AppendicesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // GET: api/<AppendicesController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchNamedEntityDTO dto, [FromServices] IGetAppendicesQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<AppendicesController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetAppendiceQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<AppendicesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateNamedEntity dto, [FromServices] ICreateAppendiceCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<AppendicesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateNamedEntity dto, [FromServices] IUpdateAppendiceCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<AppendicesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAppendiceCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
