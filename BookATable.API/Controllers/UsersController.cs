using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPatch]
        public IActionResult ActivateAccount([FromBody] ActivateAccountDTO dto, [FromServices] IActivateAccountCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}/Admin")]
        public IActionResult DeleteAdmin(int id, [FromServices] IAdminDeleteUserCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserDTO dto, [FromServices] IUpdateUserCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

    }
}
