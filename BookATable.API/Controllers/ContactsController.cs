using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Contact;
using BookATable.Application.UseCases.Queries.Contact;
using BookATable.Application.UseCases.Queries.Dish;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public ContactsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<ContactsController>
        [Authorize]
        [HttpGet]
        public IActionResult Search([FromQuery] SearchContactDTO search, [FromServices] IGetContactsQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));


        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateContactDTO dto, [FromServices] ICreateContactCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }



        // DELETE api/<ContactsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteContactCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
