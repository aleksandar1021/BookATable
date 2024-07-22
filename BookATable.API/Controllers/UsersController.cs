using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public UsersController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDTO dto, [FromServices] IRegisterUserCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        
        
    }
}
