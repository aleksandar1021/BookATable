using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Saved;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.Application.UseCases.Queries.Saved;
using BookATable.Implementation;
using BookATable.Implementation.UseCases.Queries.Saved;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public SavedController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<SavedController>
        [Authorize]
        [HttpGet]
        public IActionResult Search([FromQuery] SearchSavedDTO search, [FromServices] IGetSavedsQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));

        // GET api/<SavedController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetSavedQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<SavedController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateSavedDTO dto, [FromServices] ICreateDeleteSavedCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

   
    }
}
