using BookATable.API.Core;
using BookATable.API.DTO;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Auth;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenCreator _tokenCreator;

        private UseCaseHandler _commandHandler;

        public AuthController(JwtTokenCreator tokenCreator, UseCaseHandler commandHandler)
        {
            _tokenCreator = tokenCreator;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public IActionResult IsLogged([FromServices] IIsLogged query, [FromQuery] SearchNamedEntityDTO dto)
            => Ok(_commandHandler.HandleQuery(query,dto));

        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest request)
        {
            string token = _tokenCreator.Create(request.Email, request.Password);

            return Ok(new AuthResponse { Token = token });
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromServices] ITokenStorage storage)
        {
            storage.Remove(this.Request.GetTokenId().Value);

            return NoContent();
        }


    }
}
