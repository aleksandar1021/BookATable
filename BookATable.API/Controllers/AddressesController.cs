using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public AddressesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<AddressesController>
        [Authorize]
        [HttpGet]
        public IActionResult Find([FromQuery] SearchAddressDTO dto, [FromServices] IGetAddressesQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<AddressesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetAddressQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<AddressesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateAddressDTO dto, [FromServices] ICreateAddressCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<AddressesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAddressDTO dto, [FromServices] IUpdateAddressCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<AddressesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAddressCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
